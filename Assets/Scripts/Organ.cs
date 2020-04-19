using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

}

public class Organ : MonoBehaviour {

    public string organName;
    public bool unlockedAtStart = true;
    public Sprite hudImage;
    public Sprite hudImageSelected;
    public Color forbiddenColor;
    public Resources.ResourcesType resourcesType;

    private Color initColor;
    private List<Color> initColors;
    private ResourceConf resourceConfCost = new ResourceConf();
    private Resources resourcesReward = new Resources(0, Resources.ResourcesType.A);
    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }

    private bool startHasBeenCalled = false;
    private bool rewardDAlreadyDone = false;
    private bool rewardAx2 = false;

    void Start() {
        startHasBeenCalled = true;
        if (GetComponent<SpriteRenderer>() != null) {
            initColor = GetComponent<SpriteRenderer>().color;
        } else {
            initColors = new List<Color>();
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
                initColors.Add(renderer.color);
            }
        }
        collideWithOtherOrgan = false;

        // Init Cost and reward Configuration
        if (resourcesType == Resources.ResourcesType.A) {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(2, Resources.ResourcesType.A);
        } else if (resourcesType == Resources.ResourcesType.B) {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.B);
        } else if (resourcesType == Resources.ResourcesType.C) {
            resourceConfCost.A.count = 1;
            resourceConfCost.B.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.C);
        } else if (resourcesType == Resources.ResourcesType.D) {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.D);
        }
    }

    bool Buy(Resources.ResourcesType type) {
        return GameManager.GetInstance().GetResources(resourceConfCost.A.type).Use(resourceConfCost.A.count) &
               GameManager.GetInstance().GetResources(resourceConfCost.B.type).Use(resourceConfCost.B.count) &
               GameManager.GetInstance().GetResources(resourceConfCost.C.type).Use(resourceConfCost.C.count) &
               GameManager.GetInstance().GetResources(resourceConfCost.D.type).Use(resourceConfCost.D.count);
    }

    public void SetToForbiddenColor() {
        if (GetComponent<SpriteRenderer>() != null) {
            GetComponent<SpriteRenderer>().color = forbiddenColor;
        } else {
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
                renderer.color = forbiddenColor;
            }
        }
    }

    public void RevertColor() {
        if (startHasBeenCalled) {
            if (GetComponent<SpriteRenderer>() != null) {
                GetComponent<SpriteRenderer>().color = initColor;
            } else {
                int i = 0;
                foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
                    renderer.color = initColors[i];
                    i++;
                }
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Organ") {
            collideWithOtherOrgan = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Organ") {
            collideWithOtherOrgan = false;
        }
    }

    public void OnReward() {
        // make the reward only tje first time for resourceD
        if (rewardDAlreadyDone) {
            return;
        }
        int reward = resourcesReward.count;
        if (rewardAx2) {
            reward = reward * 2;
        }
        if (resourcesType == Resources.ResourcesType.D) {
            
            rewardDAlreadyDone = true;
        } else if  (resourcesType == Resources.ResourcesType.A) {
            rewardAx2 = false;
        }
        GameManager.GetInstance().GetResources(resourcesReward.type).Add(resourcesReward.count);
    }
}
