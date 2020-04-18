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
    private ResourceConf resourceConfCost = new ResourceConf();
    private Resources resourcesReward = new Resources(0, Resources.ResourcesType.A);
    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }

    private bool startHasBeenCalled = false;
    private bool rewardDAlreadyDone = false;

    void Start() {
        startHasBeenCalled = true;
        initColor = GetComponent<SpriteRenderer>().color;
        collideWithOtherOrgan = false;

        // Init Cost and reward Configuration
        if (resourcesType == Resources.ResourcesType.A)
        {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(2, Resources.ResourcesType.A);
        }
        else if (resourcesType == Resources.ResourcesType.B)
        {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.B);
        }
        else if (resourcesType == Resources.ResourcesType.C)
        {
            resourceConfCost.A.count = 1;
            resourceConfCost.B.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.C);
        }
        else if (resourcesType == Resources.ResourcesType.D)
        {
            resourceConfCost.A.count = 1;
            resourcesReward.Set(1, Resources.ResourcesType.D);
        }
    }

    public void SetToForbiddenColor() {
        GetComponent<SpriteRenderer>().color = forbiddenColor;
    }

    public void RevertColor() {
        if (startHasBeenCalled) {
            GetComponent<SpriteRenderer>().color = initColor;
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
        if (resourcesType == Resources.ResourcesType.D) {
            // make the reward only tje first time for resourceD
            if (rewardDAlreadyDone)
                return;
            rewardDAlreadyDone = true;
            GameManager.GetInstance().GetResources(resourcesReward.type).Add(resourcesReward.count);
        }
        else {
            GameManager.GetInstance().GetResources(resourcesReward.type).Add(resourcesReward.count);
            GameManager.GetInstance().GetResources(resourceConfCost.A.type).Use(resourceConfCost.A.count);
            GameManager.GetInstance().GetResources(resourceConfCost.B.type).Use(resourceConfCost.B.count);
            GameManager.GetInstance().GetResources(resourceConfCost.C.type).Use(resourceConfCost.C.count);
        }
        
    }
}
