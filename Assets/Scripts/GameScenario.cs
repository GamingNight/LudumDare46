using UnityEngine;

public class GameScenario : MonoBehaviour {

    private static GameScenario INSTANCE;
    private static bool _TUTORIAL = true;
    public static bool IS_TUTORIAL { get { return _TUTORIAL; } }


    public enum StateName {
        FIRST_TEST, CONGRATS_FIRST_TEST, INCREASE_DEFENSE
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

    void Start() {

        Init();
    }

    public void Init() {

        _TUTORIAL = true;
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
            tutoPanels[currentState].SetActive(true);
            if (currentState == 1) {
                triggerTimer = true;
                timerDuration = 4;
                currentTime = 0;
                stateAfterTimer = StateName.INCREASE_DEFENSE;
            }
        }
        stateAccomplished = true;
        if (currentState == 8) {
            _TUTORIAL = false;
        }
    }

    public void SkipTutorial() {

        Init();
        _TUTORIAL = false;
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
            default:
                break;
        }
        stateAccomplished = false;
    }
}
