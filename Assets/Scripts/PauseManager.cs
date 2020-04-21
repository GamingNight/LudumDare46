using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	private bool paused = false;
	public MenuNavig nav;

    public void Init()
    {
        paused = false;
    }

    private void PauseGame() {
        Time.timeScale = 0;
        nav.gameObject.SetActive(true);
        nav.PauseMenu();
    }

    private void ResumeGame() {
        Time.timeScale = 1;
        nav.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                PauseGame();
                paused = true;
            } else {
                ResumeGame();
                paused = false;
            }
        }
    }
}
