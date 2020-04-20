using UnityEngine;

public class GameScenario : MonoBehaviour {

    private readonly int nbStates = 9;

    private int currentState;
    private bool stateAccomplished;

    void Start() {

        Init();
    }

    public void Init() {

        currentState = 0;
        stateAccomplished = false;
    }

    void Update() {

        if (stateAccomplished) {
            return;
        }

        if (currentState == 0) {

        }
        stateAccomplished = true;
    }

    public void GoToNextState() {

        currentState++;
        stateAccomplished = false;
    }
}
