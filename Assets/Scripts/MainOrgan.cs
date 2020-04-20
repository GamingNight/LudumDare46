﻿using UnityEngine;
using UnityEngine.UI;

public class MainOrgan : MonoBehaviour {

    public bool IsSelected { get { return isSelected; } }
    public GameObject generateButtonActivated;
    public GameObject generateButtonDeactivated;
    public GameObject nextVirusAlarmPrefab;
    public GameObject nextTurnButton;

    private bool collideWithOtherOrgan;
    public bool CollideWithOtherOrgan { get { return collideWithOtherOrgan; } }
    private Animator animator;
    private bool isSelected;
    private GameObject nextVirusAlarm;

    void Start() {
        collideWithOtherOrgan = false;
        animator = GetComponent<Animator>();
        isSelected = false;
        UpdateGenerateButton();
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
        nextTurnButton.GetComponentInChildren<Text>().text = "Testing...";
        nextTurnButton.GetComponentInChildren<Animator>().SetTrigger("fillButton");
    }

    public void EndNextVirusSample() {
        if (GameManager.GetInstance().GoToNextTurn()) {
            nextTurnButton.GetComponentInChildren<Text>().text = "Success!";
            nextTurnButton.GetComponentInChildren<Animator>().SetTrigger("success");
            Destroy(nextVirusAlarm);
            nextTurnButton.GetComponent<AudioSource>().Play();
        }
    }

    public void EndSuccessVirusSample() {
        nextTurnButton.GetComponentInChildren<Text>().text = ">  Submit to next virus sample  <";
    }
}
