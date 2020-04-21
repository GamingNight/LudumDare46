using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Organ : MonoBehaviour {

    public string organName;
    public bool unlockedAtStart = true;
    public Sprite hudImage;
    public Sprite hudImageSelected;
    public Sprite hudImageWrong;
    public Color forbiddenColor;
    public Resources.ResourcesType resourcesType;
    public GameObject toggleButtonA;
    public GameObject shape3D;
    public GameObject boostedCube;

    private Color initColor;
    private List<Color> initColors;
    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }

    private bool startHasBeenCalled = false;
    private bool rewardx2 = false;
    private bool BoostOnTurn = false;

    private Animator animator;

    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }

    private int buildTurn = 0;

    void Start() {
        startHasBeenCalled = true;
        if (GetComponent<SpriteRenderer>() != null) {
            initColor = GetComponent<SpriteRenderer>().color;
        } else {
            initColors = new List<Color>();
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
                initColors.Add(renderer.color);
            }
        }
        collideWithOtherOrgan = false;

        animator = GetComponent<Animator>();
        isSelected = false;
        GameManager.GetInstance().UpdateSimulation();
        if (toggleButtonA) {
            toggleButtonA.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ToggleBoost(); });
        }
        buildTurn = GameManager.GetInstance().roundCount;
    }

    public int GetBuildTurn() {
        return buildTurn;
    }
    public void SetToForbiddenLook() {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
            renderer.color = forbiddenColor;
        }
        shape3D.SetActive(false);
    }

    public void RevertLook() {
        if (startHasBeenCalled) {
            int i = 0;
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
                renderer.color = initColors[i];
                i++;
            }
            shape3D.SetActive(true);
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

    public void OnSimulateReward() {
        if (rewardx2) {
            GameManager.GetInstance().SimulationAdd(resourcesType);
        }
        GameManager.GetInstance().SimulationAdd(resourcesType);
    }

    public void OnGoToNextTurn() {
        // rewardx2 = false;
        // if (toggleButtonA) {
        //    toggleButtonA.GetComponent<Toggle>().isOn = false;
        BoostOnTurn = false;
    }

    public void OnResetTurn() {
        if (!BoostOnTurn)
            return;

        Toggle button = toggleButtonA.GetComponent<Toggle>();
        bool status = button.isOn;
        button.isOn = !status;
        BoostOnTurn = false;
    }

    void OnMouseEnter() {

        if (animator != null) {
            animator.SetBool("selected", true);
        }
        CursorManager.GetInstance().TriggerSelectionCursor();
        isSelected = true;
    }

    void OnMouseOver() {

        CursorManager.GetInstance().TriggerSelectionCursor();
    }

    void OnMouseExit() {

        if (animator != null) {
            animator.SetBool("selected", false);
        }
        CursorManager.GetInstance().TriggerNavigationCursorFromOrgan();
        isSelected = false;
    }

    private void UpdateBoostedCube(bool status) {
        if (boostedCube) {
            boostedCube.SetActive(status);
        }
    }

    public void ToggleBoost() {
        Toggle button = toggleButtonA.GetComponent<Toggle>();
        bool status = button.isOn;
        if (status) {
            if (GameManager.GetInstance().BuyBoost()) {
                rewardx2 = true;
                BoostOnTurn = true;
                UpdateBoostedCube(true);
            } else {
                toggleButtonA.GetComponent<Toggle>().isOn = false;
                UpdateBoostedCube(false);
            }
        } else if (rewardx2) {
            GameManager.GetInstance().RefundBoost();
            rewardx2 = false;
            BoostOnTurn = false;
            UpdateBoostedCube(false);
        }

        GameManager.GetInstance().UpdateSimulation();
    }
}
