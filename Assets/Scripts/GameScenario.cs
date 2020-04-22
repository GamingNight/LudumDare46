using UnityEngine;

public class GameScenario : MonoBehaviour {

    private static GameScenario INSTANCE;
    private static bool _TUTORIAL = true;
    public static bool IS_TUTORIAL { get { return _TUTORIAL; } }

    public static readonly int FIRST_TEST = 0;
    public static readonly int CONGRATS_FIRST_TEST = 1;
    public static readonly int INCREASE_DEFENSE = 2;
    public static readonly int COST_BLUE = 3;
    public static readonly int RUNNING_OUT_OF_BLUE = 4;
    public static readonly int CLICK_MAP = 5;
    public static readonly int EXPLAIN_MAIN_ORGAN_COST = 6;
    public static readonly int IT_S_YOUR_TURN = 7;
    public static readonly int END_TUTO = 8;

    public GameObject tutoCanvas;

    private int currentState;
    private bool stateAccomplished;
    private GameObject[] tutoPanels;

    private bool triggerTimer;
    private float timerDuration;
    private float currentTime;
    private int stateAfterTimer;

    public static GameScenario GetInstance() {

        return INSTANCE;
    }

    void Awake() {

        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    public void Init() {

        _TUTORIAL = true;
        currentState = 0;
        stateAccomplished = false;
        int i = 0;
        tutoPanels = new GameObject[tutoCanvas.transform.childCount];
        foreach (Transform t in tutoCanvas.transform) {
            if (t.name.Contains("Tuto")) {
                tutoPanels[i] = t.gameObject;
                t.gameObject.SetActive(false);
                i++;
            }
        }
        tutoCanvas.transform.Find("FakeDefense").gameObject.SetActive(true);
        tutoCanvas.transform.Find("FakeDefenseCost").gameObject.SetActive(true);
    }

    public void SkipTutorial() {
        Debug.Log("Skip tuto");
        _TUTORIAL = false;
        currentState = 8;
        int i = 0;
        tutoPanels = new GameObject[tutoCanvas.transform.childCount];
        foreach (Transform t in tutoCanvas.transform) {
            tutoPanels[i] = t.gameObject;
            t.gameObject.SetActive(false);
            i++;
        }
        tutoCanvas.transform.Find("FakeDefense").gameObject.SetActive(false);
        tutoCanvas.transform.Find("FakeDefenseCost").gameObject.SetActive(false);
    }

    void Update() {

        if (triggerTimer) {
            if (currentTime >= timerDuration) {
                ReachState(stateAfterTimer);
                triggerTimer = false;
            } else {
                currentTime += Time.deltaTime;
            }
        }

        if (stateAccomplished || !_TUTORIAL) {
            return;
        }

        if (currentState == FIRST_TEST) {
            tutoPanels[0].SetActive(true);
            stateAccomplished = true;
        } else {
            tutoPanels[currentState - 1].SetActive(false);
            if (currentState < END_TUTO) {
                tutoPanels[currentState].SetActive(true);
            }
            if (currentState == CONGRATS_FIRST_TEST) {
                triggerTimer = true;
                timerDuration = 4;
                currentTime = 0;
                stateAfterTimer = INCREASE_DEFENSE;
            } else if (currentState == COST_BLUE) {
                HideFakeDefensePanels();
                triggerTimer = true;
                timerDuration = 5;
                currentTime = 0;
                stateAfterTimer = RUNNING_OUT_OF_BLUE;
            } else if (currentState == CLICK_MAP) {
                triggerTimer = true;
                timerDuration = 5;
                currentTime = 0;
                stateAfterTimer = EXPLAIN_MAIN_ORGAN_COST;
            } else if (currentState == EXPLAIN_MAIN_ORGAN_COST) {
                triggerTimer = true;
                timerDuration = 5;
                currentTime = 0;
                stateAfterTimer = IT_S_YOUR_TURN;
            } else if (currentState == IT_S_YOUR_TURN) {
                triggerTimer = true;
                timerDuration = 3;
                currentTime = 0;
                stateAfterTimer = END_TUTO;
            }
        }
        stateAccomplished = true;
        if (currentState == 8) {
            _TUTORIAL = false;
        }
    }

    public void ReachState(int state) {

        if (state != currentState + 1) {
            return;
        }
        currentState = state;
        stateAccomplished = false;
    }

    private void HideFakeDefensePanels() {
        tutoCanvas.transform.Find("FakeDefense").gameObject.SetActive(false);
        tutoCanvas.transform.Find("FakeDefenseCost").gameObject.SetActive(false);
    }

    public int GetCurrentState() {

        return currentState;
    }
}
