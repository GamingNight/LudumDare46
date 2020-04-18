using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }
}
