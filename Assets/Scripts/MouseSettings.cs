using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    [SerializeField] private Slider mouseSettingSlider;
    string mouseSettings = "MouseSensitivitySetting";
    
    private void Awake()
    {
        mouseSettingSlider.value = PlayerPrefs.GetFloat(mouseSettings);
    }

    public void MouseSensitivityChange()
    {
        PlayerPrefs.SetFloat(mouseSettings, mouseSettingSlider.value);
        PlayerPrefs.Save();
    }
}
