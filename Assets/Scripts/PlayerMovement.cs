using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    
    //Player Movement Speed
    [Range(1,100)]
    public float speed = 12f;

    //Gravity and Multiplier to give a snappy fall
    public float gravity = -9.81f;
   
    //Change for faster fall
    public float fallMultiplier = 2.5f;

    //Jump Height setting with range slider
    [Range(1,100)]
    public float jumpHeight = 3f;

    //
    public Transform groundCheck;
   
    //Sphere that will be used as the collider?
    public float groundDistance = 0.4f;
    
    //what will be considered ground
    public LayerMask groundMask;

    private bool canDoubleJump;
    private Vector3 move;

    Vector3 velocity;
    bool isGrounded;
    bool isGroundedcheck;

    private void OnEnable()
    {
        isGrounded = true;
        isGroundedcheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            GetPlayerInput();
        }
        else
        {
            velocity.y = -2f;
            move = Vector3.zero;
        }

    }


    private void GetPlayerInput() 
    { 
        //Checking if player is in contact with ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                 
        if(isGrounded != isGroundedcheck)
        {
            isGroundedcheck = isGrounded;
            if (isGrounded == true)
            {
                AudioManager.instance.Play("LandSound");
            }
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        //Making sure player reaches ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Getting w,a,s,d input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Defining "move" vector
        Vector3 move = transform.right * x + transform.forward * z;

        //The actual movement code
        controller.Move(move * speed * Time.deltaTime);


        //Jump code
        if(Input.GetButtonDown("Jump")) 
        {
            if (isGrounded)
            {
                AudioManager.instance.Play("JumpSound");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else
            {
                if (canDoubleJump)
                {
                    AudioManager.instance.Play("JumpSound");
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    canDoubleJump = false;
                }
            }
        }
        
        //Fall 
        velocity.y += gravity * (fallMultiplier-1f) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

      

    }
}
