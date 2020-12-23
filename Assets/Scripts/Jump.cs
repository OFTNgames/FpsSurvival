using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Assigned in Inspector")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
        
    [Header("Jump Values")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _fallMultiplier = 2.5f; //Change for faster fall
    [Range(1, 100)] [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _groundDistance = 0.4f;

    private CharacterController _controller;
    private bool _canDoubleJump;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isGroundedcheck;

    private void Awake() => _controller = GetComponent<CharacterController>();

    private void OnEnable()
    {
        _isGrounded = true;
        _isGroundedcheck = true;
    }

    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            CheckIfPlayerIsGrounded();
            PlayerJump();
        }
        else
        {
            _velocity.y = -2f;
        }
    }

    private void CheckIfPlayerIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded != _isGroundedcheck)
        {
            _isGroundedcheck = _isGrounded;
            if (_isGrounded == true)
            {
                AudioManager.instance.Play("LandSound");
            }
        }

        if (_isGrounded)
        {
            _canDoubleJump = true;
        }

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                AudioManager.instance.Play("JumpSound");
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
            else
            {
                if (_canDoubleJump)
                {
                    AudioManager.instance.Play("JumpSound");
                    _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                    _canDoubleJump = false;
                }
            }
        }
        _velocity.y += _gravity * (_fallMultiplier - 1f) * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
