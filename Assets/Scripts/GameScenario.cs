using UnityEngine;

public class GameScenario : MonoBehaviour {

    private readonly int nbStates = 9;

    public GameObject tutoCanvas;

    private int currentState;
    private bool stateAccomplished;
    private GameObject[] tutoPanels;

    void Start() {

        Init();
    }

    public void Init() {

        currentState = 0;
        stateAccomplished = false;
        int i = 0;
        tutoPanels = new GameObject[tutoCanvas.transform.childCount];
        foreach (Transform t in tutoCanvas.transform) {
            tutoPanels[i] = t.gameObject;
            t.gameObject.SetActive(false);
            i++;
        }
    }

    void Update() {

        if (stateAccomplished) {
            return;
        }

        if (currentState == 0) {
            tutoPanels[currentState].SetActive(true);
        }
        stateAccomplished = true;
    }

    public void GoToNextState() {

        currentState++;
        stateAccomplished = false;
    }
}
