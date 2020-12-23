using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Range(1,100)][SerializeField] private float _moveSpeed = 12f;

    private CharacterController _controller;
    private float _horizontal;
    private float _vertical;
    
    private void Awake() => _controller = GetComponent<CharacterController>();

    private void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            GetInput();
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical).normalized;
        Vector3 movement = transform.TransformDirection(direction) * _moveSpeed;
        _controller.Move(movement * Time.deltaTime);
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
}
