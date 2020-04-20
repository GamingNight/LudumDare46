using UnityEngine;

public class MenuNavig : MonoBehaviour {
    public GameObject welcomeText;
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject restartButton;
    public GameObject gameoverText;
    public GameObject winText;

    public GameObject cursor;
    public GameObject gameManager;
    public GameObject organContainer;

    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
        SetMainGameActive(true);
        ResetGame(false);
        gameObject.SetActive(false);
    }

    public void StartGameWithTutorial() {
        SetMainGameActive(true);
        ResetGame(true);
        gameObject.SetActive(false);
    }

    public void WelcomeMenu() {
        startButton.SetActive(true);
        tutorialButton.SetActive(true);
        restartButton.SetActive(false);
        gameoverText.SetActive(false);
        winText.SetActive(false);
        welcomeText.SetActive(true);
    }

    public void EndMenu() {
        SetMainGameActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        restartButton.SetActive(true);
        gameoverText.SetActive(true);
        winText.SetActive(false);
        welcomeText.SetActive(false);
    }

    public void WinMenu() {
        SetMainGameActive(false);
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        restartButton.SetActive(true);
        gameoverText.SetActive(false);
        winText.SetActive(true);
        welcomeText.SetActive(false);
    }

    private void SetMainGameActive(bool b) {
        cursor.SetActive(b);
        gameManager.SetActive(b);
        organContainer.SetActive(b);
        Camera.main.GetComponent<MoveCamera>().enabled = b;
    }

    private void ResetGame(bool withTuto) {

        cursor.GetComponent<CursorManager>().Init();
        gameManager.GetComponent<OrganSettlementManager>().Init();
        gameManager.GetComponent<GameManager>().Reset();
        if (withTuto) {
            gameManager.GetComponent<GameScenario>().Init();
        } else {
            gameManager.GetComponent<GameScenario>().SkipTutorial();
        }
        LineDrawer.ClearRelations();
        Camera.main.GetComponent<MoveCamera>().Init();
    }
}
