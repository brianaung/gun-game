using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public float playerSpeed = 9.0f;
    public float jumpStrength = 9.0f;
    public float dashStrength = 20.0f;
    public float sensitivity = 1.0f;
    public float dashCooldownTime = 2f;
    public float dashTime = 0.3f;
    private Transform playerTransform;
    public float gravity = 25f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool doubleJump = false;
    private bool hasJumped = false;
    private float dashTimer;
    public ParticleSystem dust;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource highjump;
    [SerializeField] AudioSource walking;
    [SerializeField] AudioSource dash;
    private void Awake() 
    {
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
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
        if(characterController.isGrounded && (move.x != 0 || move.z != 0) && walking.isPlaying == false){
            walking.volume = Random.Range(0.2f, 0.5f);
            walking.pitch = Random.Range(0.7f, 0.9f);
            walking.Play();
        }
        var direction = Vector3.Normalize(move);
        
        dashTimer += Time.deltaTime;

        if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && dashTimer > dashCooldownTime)
        {
            StartCoroutine(dashCoroutine(direction));
            dashTimer = 0f;
            dash.Play();
        }

        Jump();

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     GameManager.Instance.loadBossScene();
        // }
    }

    public void handleRotation() 
    {
        playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity,0));
    }
    public void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!hasJumped) {
                hasJumped = true;
                doubleJump = true;
                velocity.y = jumpStrength;
                if(jumpStrength >= 18.0f) highjump.Play();
                else jump.Play();
                dust.Play();
            }

            else if(doubleJump)
            {
                velocity.y = jumpStrength;
                doubleJump = false;
                if(jumpStrength >= 18.0f)highjump.Play(); 
                else jump.Play();
                dust.Play();
            }
        }
    }

    IEnumerator dashCoroutine(Vector3 direction)
    {
        var startTime = Time.time;

        while(Time.time - startTime < dashTime)
        {
            characterController.Move(direction * dashStrength * Time.deltaTime);
            yield return null;
        }
        
    }
}
