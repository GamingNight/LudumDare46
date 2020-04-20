using UnityEngine;

public class CursorManager : MonoBehaviour {

    private static CursorManager INSTANCE;

    public static CursorManager GetInstance() {

        return INSTANCE;
    }

    public GameObject staticCursorPrefab;
    private Animator animator;

    private GameObject staticCursor;
    private bool fromOrganSettlementManager;

    void Awake() {

        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {

        animator = GetComponent<Animator>();
        Init();
    }

    public void Init() {

        fromOrganSettlementManager = false;
        DestroyStaticCursor();
    }

    void OnDisable() {
        Cursor.visible = true;
        if (staticCursor != null) {
            Destroy(staticCursor);
        }
    }

    void Update() {
        Cursor.visible = false;
        Vector3 mouseWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray)) {
            if (hit.transform.tag == "Ground") {
                mouseWorldPosition = hit.point;
            }
        }
        transform.position = new Vector3(mouseWorldPosition.x, 0.1f, mouseWorldPosition.z);
    }

    //Called by an Organ whenever the cursor comes over it
    public void TriggerSelectionCursor() {

        if (fromOrganSettlementManager) { //selection cursor is already active
            return;
        }
        fromOrganSettlementManager = false;
        animator.SetBool("selectionIsActive", true);
    }

    //Called by the OrganSettlementManager to trigger the "Select Menu" cursor
    public void TriggerSelectionMenuCursor() {

        fromOrganSettlementManager = true;
        animator.SetBool("selectionIsActive", true);
        staticCursor = Instantiate<GameObject>(staticCursorPrefab);
        staticCursor.transform.position = transform.position;
    }

    //Called by an Organ as the cursor moves away from it.
    public void TriggerNavigationCursorFromOrgan() {

        //Don't go back to the initial state if we are on a selection state from the OrganSettlementManager.
        if (fromOrganSettlementManager) {
            return;
        }
        animator.SetBool("selectionIsActive", false);
    }

    //Called by the OrganSettlementManager when the "Select Menu" is done.
    public void TriggerNavigationCursorFromSettlementManager() {

        fromOrganSettlementManager = false;
        animator.SetBool("selectionIsActive", false);
        DestroyStaticCursor();
    }

    public void DestroyStaticCursor() {
        if (staticCursor != null) {
            Destroy(staticCursor);
        }
    }
}
