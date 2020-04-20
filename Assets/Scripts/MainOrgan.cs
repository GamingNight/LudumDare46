using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class MainOrgan : MonoBehaviour {

    public bool IsSelected { get { return isSelected; } }
    public GameObject generateButtonActivated;
    public GameObject generateButtonDeactivated;
    public GameObject nextVirusAlarmPrefab;
    public GameObject nextTurnButton;
    public GameObject menuCanvas;
    public GameObject increaseButton;

    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }
    private Animator animator;
    private bool isSelected;
    private GameObject nextVirusAlarm;
    private Coroutine increaseButtonColorCoroutine;
    private Color increaseButtonInitColor;

    void Start() {
        collideWithOtherOrgan = false;
        animator = GetComponent<Animator>();
        isSelected = false;
        UpdateGenerateButton();
    }

    public void Init() {

    }

    private IEnumerator ChangeIncreaseButtonColor() {
        increaseButtonInitColor = increaseButton.GetComponent<Image>().color;
        increaseButton.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        increaseButton.GetComponent<Image>().color = increaseButtonInitColor;
    }

    public void BuyDef() {

        if (GameManager.GetInstance().CanBuyDef()) {
            GameManager.GetInstance().BuyDef();
        } else if (increaseButton) {
            if (increaseButtonColorCoroutine != null) {
                StopCoroutine(increaseButtonColorCoroutine);
                increaseButton.GetComponent<Image>().color = increaseButtonInitColor;
            }
            increaseButtonColorCoroutine = StartCoroutine(ChangeIncreaseButtonColor());
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

    void OnMouseEnter() {

        if (animator != null) {
            animator.SetBool("selected", true);
        }
        CursorManager.GetInstance().TriggerSelectionCursor();
        isSelected = true;
    }

    void OnMouseExit() {

        if (animator != null) {
            animator.SetBool("selected", false);
        }
        CursorManager.GetInstance().TriggerNavigationCursorFromOrgan();
        isSelected = false;
    }

    void OnMouseOver() {

        CursorManager.GetInstance().TriggerSelectionCursor();
    }

    private void UpdateGenerateButton() {
        if (!generateButtonActivated) {
            return;
        }

        if (!generateButtonDeactivated) {
            return;
        }

        bool canBuy = GameManager.GetInstance().CanGenerate();
        generateButtonActivated.SetActive(canBuy);
        generateButtonDeactivated.SetActive(!canBuy);
    }

    public void OnGoToNextTurn() {
        UpdateGenerateButton();
    }

    public void TriggerNextVirusSampleAnimation() {

        nextVirusAlarm = Instantiate<GameObject>(nextVirusAlarmPrefab);

        nextTurnButton.GetComponentInChildren<Text>().text = "testing with level " + GameManager.GetInstance().GetTestLevel().ToString() + " virus sample";
        nextTurnButton.GetComponentInChildren<Animator>().SetTrigger("fillButton");
    }

    public void EndNextVirusSample() {

        if (GameScenario.IS_TUTORIAL) {
            ShowSuccess();
        } else {
            if (GameManager.GetInstance().GoToNextTurn()) {
                ShowSuccess();
            } else {
                TriggerEndGame();
            }
        }

    }

    private void ShowSuccess() {
        nextTurnButton.GetComponentInChildren<Text>().text = "Success!";
        nextTurnButton.GetComponentInChildren<Animator>().SetTrigger("success");
        nextTurnButton.GetComponent<AudioSource>().Play();
        Destroy(nextVirusAlarm);
    }

    public void EndSuccessVirusSample() {
        nextTurnButton.GetComponentInChildren<Text>().text = ">  Submit to next virus sample  <";
        if (GameScenario.IS_TUTORIAL) {
            GameScenario.GetInstance().ReachState(GameScenario.StateName.CONGRATS_FIRST_TEST);
        }
    }

    private void TriggerEndGame() {
        nextTurnButton.GetComponentInChildren<Text>().text = ">  Submit to next virus sample  <";
        menuCanvas.SetActive(true);
        menuCanvas.GetComponent<MenuNavig>().EndMenu();
        Destroy(nextVirusAlarm);
    }

    private void TriggerSuccesGame() {
        nextTurnButton.GetComponentInChildren<Text>().text = ">  Submit to next virus sample  <";
        menuCanvas.SetActive(true);
        menuCanvas.GetComponent<MenuNavig>().EndMenu();
        Destroy(nextVirusAlarm);
    }

}
