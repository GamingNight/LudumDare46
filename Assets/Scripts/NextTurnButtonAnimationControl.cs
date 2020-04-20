using UnityEngine;

public class NextTurnButtonAnimationControl : MonoBehaviour {

    public MainOrgan mainOrgan;

    public void ReachedEndOfAnimation() {
        mainOrgan.EndNextVirusSample();
    }
}
