using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    private Transform cameraTransform;
    private float xRotation = 0f;
    private string mouseSettings = "MouseSensitivitySetting";

    private void Awake() => cameraTransform = Camera.main.transform;

    private void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat(mouseSettings);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            MouseLook();
        }
    }

    private void MouseLook()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
        
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85, 85);
        cameraTransform.localEulerAngles = Vector3.right * xRotation;
    }
}
