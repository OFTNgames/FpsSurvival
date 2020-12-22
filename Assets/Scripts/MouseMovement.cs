using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public float mouseSensitivity = 100f;
    public GameObject gameOver;
    public Transform playerbody;

    float xRotation = 0f;
    string mouseSettings = "MouseSensitivitySetting";


    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat(mouseSettings);
        mouseSensitivity = mouseSensitivitySlider.value;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOver = GameObject.Find("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            MouseLook();
            
        }
        //if(gameOver.activeInHierarchy == true)
        //{
        //    Cursor.lockState = CursorLockMode.Confined;        }
   

    }

    public void MouseSensitivityChange()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        PlayerPrefs.SetFloat(mouseSettings, mouseSensitivitySlider.value);
        PlayerPrefs.Save();

    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);

    }


}
