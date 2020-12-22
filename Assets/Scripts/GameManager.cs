using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel, hudContainer, pauseMenu, optionsMenu;
    public float restartDelay = 2f;
    [SerializeField]
    float gameOverDelay = 2.5f;
    public bool gameHasEnded { get; private set; }
    public bool gamePlaying { get; private set; }
    public int enemiesActive;
    //private float startTime;



    private void Start()
    {
        instance = this;
        gamePlaying = false;
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        AudioManager.instance.Play("GamePlayMusic");
        //gameHasEnded = false;
    }

    public void BeginGame()
    {
        gamePlaying = true;
        //startTime = Time.time;
        TimerController.instance.BeginTimer();
        Debug.Log("GameStart");        
    }

    public void Update()
    {
       // enemiesActive = GameObject.FindGameObjectsWithTag"".Count
    }


    public void GameOver()
    {
       // if (gameHasEnded == false && gamePlaying == true)
       if (gamePlaying == true)
        {
            //gameHasEnded = true;
            gamePlaying = false;
            Debug.Log("GAME OVER");
            Invoke("ShowGameOverScreen", gameOverDelay);
            //Invoke("Restart", restartDelay);
        }
       
    }

    private void ShowGameOverScreen()
    {
        TimerController.instance.EndTimer();
        KillcounterController.instance.PrintEndGameScore();
        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);
        AudioManager.instance.StopPlaying("GamePlayMusic");
        AudioManager.instance.Play("MenuMusic");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HoverButton()
    {
        AudioManager.instance.Play("ButtonHover");
    }

    public void OnButtonOption()
    {
        AudioManager.instance.Play("ButtonClick");
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnButtonBack()
    {
        AudioManager.instance.Play("ButtonClick");
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }

    public void OnButtonRestart()
    {
        AudioManager.instance.Play("ButtonClick");
        AudioManager.instance.StopPlaying("MenuMusic");
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    
    public void OnButtonMainMenu()
    {
        AudioManager.instance.Play("ButtonClick");
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");

    }

   

}
