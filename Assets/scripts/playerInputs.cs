using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    
    public float playerSpeed = 5.0f;
    public float jumpStrength = 9.0f;
    public float dashStrength = 5.0f;
    public float sensitivity = 1.0f;
    public float dashCooldownTime = 1f;
    private Transform playerTransform;
    public float gravity = 25f;
    private CharacterController characterController;
    private Vector3 velocity;
    private Vector3 dashVelocity;
    private bool doubleJump = false;
    private bool hasJumped = false;
    public float dashTime = 0.1f;

    public float timer;
    private void Awake() 
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start() 
    {
        velocity = Vector3.zero;
    }

    public void handleMovement() 
    {
        // character movement from https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            hasJumped = false;
            doubleJump = false;
        }

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var move = moveDirection * playerSpeed;
        // player transform rotation from https://www.youtube.com/watch?v=CSuvGGiC2wI
        move = transform.rotation * move;
        characterController.Move(move * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            if(!hasJumped) {
                hasJumped = true;
                doubleJump = true;
                velocity.y = jumpStrength;
            }

            else if(doubleJump)
            {
                velocity.y = jumpStrength;
                doubleJump = false;
            }
        }

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
        timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && timer > dashCooldownTime)
        {
            timer = 0;
            StartCoroutine(Dash(move));
        }

        Debug.Log(dashCooldownTime);
    }

    public void handleRotation() 
    {
        playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity,0));
    }

    
    IEnumerator Dash(Vector3 moveDirection) 
    {
        var startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {

            characterController.Move(moveDirection * dashStrength * Time.deltaTime);
            yield return null;
        }
    }
}
