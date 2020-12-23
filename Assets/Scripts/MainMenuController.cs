using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public GameObject mainScreen, aboutScreen, optionScreen;
    public Slider mouseSensitivitySlider;
    [SerializeField] private Text _bestScore;
    string mouseSettings = "MouseSensitivitySetting";

    public void Start()
    {
        AudioManager.instance.Play("MenuMusic");
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat(mouseSettings);
        _bestScore.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void HoverButton()
    {
        AudioManager.instance.Play("ButtonHover");
    }
    public void OnButtonPlayGame()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene("Scene1");
        AudioManager.instance.StopPlaying("MenuMusic");
    }

    public void OnButtonOptionsMenu()
    {
        AudioManager.instance.Play("ButtonClick");
        mainScreen.SetActive(false);
        optionScreen.SetActive(true);
    }
           
    public void OnButtonAboutGame()
    {
        AudioManager.instance.Play("ButtonClick");
        mainScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }
    
    public void OnButtonBack()
    {
        AudioManager.instance.Play("ButtonClick");
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
        optionScreen.SetActive(false);
    }
       
    public void OnButtonQuit()
    {
        AudioManager.instance.Play("ButtonClick");
        Application.Quit();
    }

    public void MouseSensitivityChange()
    {
        PlayerPrefs.SetFloat(mouseSettings, mouseSensitivitySlider.value);
        PlayerPrefs.Save();
    }
}
