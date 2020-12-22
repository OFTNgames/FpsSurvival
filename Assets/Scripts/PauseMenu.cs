using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenu;

    
    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        AudioManager.instance.Play("ButtonClick");
        AudioManager.instance.StopPlaying("MenuMusic");
        AudioManager.instance.Play("GamePlayMusic");
        pauseMenuUI.SetActive(false);
        optionMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioManager.instance.PausePlaying("GamePlayMusic");
        AudioManager.instance.Play("MenuMusic");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
}

