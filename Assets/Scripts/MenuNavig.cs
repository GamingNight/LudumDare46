using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavig : MonoBehaviour
{
    public GameObject welcomeText;
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject gameoverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Quit()
    {
        Debug.Log("pommier");
        Application.Quit();
    }
    public void start()
    {
        gameObject.SetActive(false);
    }
    public void welcomeMenu()
    {
        startButton.SetActive(true);
        restartButton.SetActive(false);
        gameoverText.SetActive(false);
        welcomeText.SetActive(true);
    }
    public void endMenu()
    {
        startButton.SetActive(false);
        restartButton.SetActive(true);
        gameoverText.SetActive(true);
        welcomeText.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
