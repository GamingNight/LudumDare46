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
    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }

    private bool startHasBeenCalled = false;
    private bool rewardx2 = false;

    private Animator animator;

    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }

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

        animator = GetComponent<Animator>();
        isSelected = false;
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
        if (rewardx2) {
            GameManager.GetInstance().Add(resourcesType);
            rewardx2 = false;
        }
        GameManager.GetInstance().Add(resourcesType);
    }

    void OnMouseEnter() {

        if (animator != null) {
            animator.SetBool("selected", true);
        }
        CursorManager.GetInstance().TriggerSelectionCursor();
        isSelected = true;
    }

    void OnMouseExit() {

        if (animator != null) {
            animator.SetBool("selected", false);
        }
        CursorManager.GetInstance().TriggerNavigationCursor();
        isSelected = false;
    }

    void BuyDef() {
        if (GameManager.GetInstance().Buy(Resources.ResourcesType.D)) {
            GameManager.GetInstance().Add(Resources.ResourcesType.D);
        }
    }

    void BuyBoost() {
        if (GameManager.GetInstance().BuyBoost()) {
            rewardx2 = true;
        }
    }

    void Update() {
        if (isSelected & (resourcesType == Resources.ResourcesType.A))
        {

            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            bool scrollup = (scrollWheel > 0f);
            if (scrollup) {
                BuyDef();
            }
            bool scrolldown = (scrollWheel < 0f);
            if (scrolldown & !rewardx2) {
                BuyBoost();
            }
        }
    }
}
