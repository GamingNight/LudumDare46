using UnityEngine;

public class GameScenario : MonoBehaviour {

    private static GameScenario INSTANCE;
    private static bool _TUTORIAL = true;
    public static bool IS_TUTORIAL { get { return _TUTORIAL; } }


    public enum StateName {
        FIRST_TEST, CONGRATS_FIRST_TEST, INCREASE_DEFENSE, COST_BLUE, RUNNING_OUT_OF_BLUE, CLICK_MAP, EXPLAIN_MAIN_ORGAN_COST, IT_S_YOUR_TURN, END_TUTO

    }

    public GameObject tutoCanvas;

    private int currentState;
    private bool stateAccomplished;
    private GameObject[] tutoPanels;

    private bool triggerTimer;
    private float timerDuration;
    private float currentTime;
    private StateName stateAfterTimer;

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

        if (currentState == 0) {
            tutoPanels[0].SetActive(true);
            stateAccomplished = true;
        } else {
            tutoPanels[currentState - 1].SetActive(false);
            if (currentState < tutoPanels.Length - 1) {
                tutoPanels[currentState].SetActive(true);
            }
            if (currentState == 1) {
                triggerTimer = true;
                timerDuration = 4;
                currentTime = 0;
                stateAfterTimer = StateName.INCREASE_DEFENSE;
            } else if (currentState == 3) {
                HideFakeDefensePanel();
                triggerTimer = true;
                timerDuration = 4;
                currentTime = 0;
                stateAfterTimer = StateName.RUNNING_OUT_OF_BLUE;
            } else if (currentState == 5) {
                triggerTimer = true;
                timerDuration = 4;
                currentTime = 0;
                stateAfterTimer = StateName.EXPLAIN_MAIN_ORGAN_COST;
            } else if (currentState == 6) {
                triggerTimer = true;
                timerDuration = 5;
                currentTime = 0;
                stateAfterTimer = StateName.IT_S_YOUR_TURN;
            } else if (currentState == 7) {
                triggerTimer = true;
                timerDuration = 3;
                currentTime = 0;
                stateAfterTimer = StateName.END_TUTO;
            }
        }
        stateAccomplished = true;
        if (currentState == 8) {
            _TUTORIAL = false;
        }
    }

    public void ReachState(StateName stateName) {

        switch (stateName) {
            case StateName.FIRST_TEST:
                currentState = 0;
                break;
            case StateName.CONGRATS_FIRST_TEST:
                currentState = 1;
                break;
            case StateName.INCREASE_DEFENSE:
                currentState = 2;
                break;
            case StateName.COST_BLUE:
                currentState = 3;
                break;
            case StateName.RUNNING_OUT_OF_BLUE:
                currentState = 4;
                break;
            case StateName.CLICK_MAP:
                currentState = 5;
                break;
            case StateName.EXPLAIN_MAIN_ORGAN_COST:
                currentState = 6;
                break;
            case StateName.IT_S_YOUR_TURN:
                currentState = 7;
                break;
            case StateName.END_TUTO:
                currentState = 8;
                break;
            default:
                break;
        }
        stateAccomplished = false;
    }

    private void HideFakeDefensePanel() {
        tutoCanvas.transform.Find("FakeDefense").gameObject.SetActive(false);
    }
}
