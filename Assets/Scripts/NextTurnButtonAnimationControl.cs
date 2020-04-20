using UnityEngine;

public class NextTurnButtonAnimationControl : MonoBehaviour {

    public MainOrgan mainOrgan;

    public void ReachedEndOfTest() {
        mainOrgan.EndNextVirusSample();
    }

    public void ReachedEndOfSuccess() {
        mainOrgan.EndSuccessVirusSample();
    }
}
