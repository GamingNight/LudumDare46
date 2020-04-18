using UnityEngine;

public class Organ : MonoBehaviour {

    public string organName;
    public bool unlockedAtStart = true;
    public Sprite hudImage;
    public Sprite hudImageSelected;
    public Color forbiddenColor;
    public Resources.ResourcesType resourcesType;

    private Color initColor;
    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }

    private bool startHasBeenCalled = false;

    void Start() {
        startHasBeenCalled = true;
        initColor = GetComponent<SpriteRenderer>().color;
        collideWithOtherOrgan = false;
    }

    public void SetToForbiddenColor() {
        GetComponent<SpriteRenderer>().color = forbiddenColor;
    }

    public void RevertColor() {
        if (startHasBeenCalled) {
            GetComponent<SpriteRenderer>().color = initColor;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Organ") {
            collideWithOtherOrgan = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Organ") {
            collideWithOtherOrgan = false;
        }
    }

    public void OnSwitchState() {
        if ( GameManager.GetInstance().roudState == GameManager.RoudState.REWARD) {
            // TODO Make reward according to the type
        }
    }
}
