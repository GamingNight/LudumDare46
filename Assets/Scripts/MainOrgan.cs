using UnityEngine;
using UnityEngine.UI;

public class MainOrgan : MonoBehaviour {

    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }
    private Animator animator;
    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }
    public GameObject GenerateButtonActivated;
    public GameObject GenerateButtonDeactivated;

    void Start() {
        collideWithOtherOrgan = false;
        animator = GetComponent<Animator>();
        isSelected = false;
        updateGenerateButton();
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
        CursorManager.GetInstance().TriggerNavigationCursorFromOrgan();
        isSelected = false;
    }

    void OnMouseOver() {

        CursorManager.GetInstance().TriggerSelectionCursor();
    }

    private void updateGenerateButton() {
        if (!GenerateButtonActivated)
            return;
        if (!GenerateButtonDeactivated)
            return;

        Button activated = GenerateButtonActivated.GetComponent<Button>();
        Button deactivated = GenerateButtonDeactivated.GetComponent<Button>();
        bool canBuy = GameManager.GetInstance().CanGenerate();
        activated.enabled = canBuy;
        deactivated.enabled = (!canBuy);
    }

    public void OnGoToNextTurn() {
        updateGenerateButton();
    }
}
