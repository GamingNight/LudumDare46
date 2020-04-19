using UnityEngine;

public class CursorManager : MonoBehaviour {

    private static CursorManager INSTANCE;

    public static CursorManager GetInstance() {

        return INSTANCE;
    }

    public GameObject staticCursorPrefab;
    private Animator animator;

    private GameObject staticCursor;

    void Awake() {

        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {

        animator = GetComponent<Animator>();
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

    public void TriggerSelectionCursor() {
        animator.SetBool("selectionIsActive", true);
    }

    public void TriggerSelectionMenuCursor() {

        animator.SetBool("selectionIsActive", true);
        staticCursor = Instantiate<GameObject>(staticCursorPrefab);
        staticCursor.transform.position = transform.position;
    }

    public void TriggerNavigationCursor() {

        animator.SetBool("selectionIsActive", false);
        DestroyStaticCursor();
    }

    public void DestroyStaticCursor() {
        if (staticCursor != null) {
            Destroy(staticCursor);
        }
    }
}
