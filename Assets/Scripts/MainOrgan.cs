using UnityEngine;
using UnityEngine.UI;

public class MainOrgan : MonoBehaviour {

    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }
    private Animator animator;
    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }
    public GameObject generateButtonActivated;
    public GameObject generateButtonDeactivated;

    void Start() {
        collideWithOtherOrgan = false;
        animator = GetComponent<Animator>();
        isSelected = false;
        UpdateGenerateButton();
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

    private void UpdateGenerateButton() {
        if (!generateButtonActivated)
            return;
        if (!generateButtonDeactivated)
            return;

        Button activated = generateButtonActivated.GetComponent<Button>();
        Button deactivated = generateButtonDeactivated.GetComponent<Button>();
        bool canBuy = GameManager.GetInstance().CanGenerate();
        activated.enabled = canBuy;
        deactivated.enabled = (!canBuy);
    }

    public void OnGoToNextTurn() {
        UpdateGenerateButton();
    }
}
