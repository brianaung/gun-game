using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1.0f;
    [SerializeField] float jumpStrength = 2.0f;
    // [SerializeField] float maxSpeed = 10.0f;

    [SerializeField] float sensitivity = 1.0f;
    private Transform playerTransform;

    private Vector3 velocity = Vector3.zero;
    private float gravity = 0.5f;
    private void Awake() 
    {
        playerTransform = GetComponent<Transform>();
    }

    public void handleMovement() 
    {
        Vector3 moveDirection = Vector3.zero;
        if(Input.GetKey(KeyCode.A)) {
            moveDirection += Vector3.left;
        }

        if(Input.GetKey(KeyCode.S)) {
            moveDirection += Vector3.back;
        }

        if(Input.GetKey(KeyCode.D)) {
            moveDirection += Vector3.right;
        }

        if(Input.GetKey(KeyCode.W)) {
            moveDirection += Vector3.forward;
        }

        Vector3 move = moveDirection * playerSpeed;
        move = transform.rotation * move; 

        // if(Input.GetKeyDown(KeyCode.Space)) {
        //     moveDirection += Vector3.up;
        // }

        move -= new Vector3(0, gravity, 0) * Time.deltaTime;

        transform.position += move * Time.deltaTime;

    }

    public void handleRotation() 
    {
        playerTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity,0));
    }
}
