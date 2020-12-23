using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel, hudContainer, pauseMenu, optionsMenu;
    [SerializeField] private Text _bestScore;
    public float restartDelay = 2f;
    
    [SerializeField]float gameOverDelay = 2.5f;
    
    public bool gameHasEnded { get; private set; }
    public bool gamePlaying { get; private set; }

    private void Start()
    {
        instance = this;
        gamePlaying = false;
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1f;
        AudioManager.instance.Play("GamePlayMusic");
    }

    public void BeginGame()
    {
        gamePlaying = true;
        TimerController.instance.BeginTimer();
    }

    public void GameOver()
    {
       if (gamePlaying == true)
       {
           gamePlaying = false;
           Invoke("ShowGameOverScreen", gameOverDelay);
       }
    }

    private void ShowGameOverScreen()
    {
        TimerController.instance.EndTimer();
        KillcounterController.instance.PrintEndGameScore();
        if(KillcounterController.instance.numberkilled > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", KillcounterController.instance.numberkilled);
            _bestScore.text = "BEST SCORE: " + KillcounterController.instance.numberkilled.ToString() + "\nNEW RECORD!";
        }
        else
        {
            _bestScore.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
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
