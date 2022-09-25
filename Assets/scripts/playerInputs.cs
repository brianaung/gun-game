using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1.0f;
    [SerializeField] float jumpStrength = 2.0f;
    // [SerializeField] float maxSpeed = 10.0f;
    new Rigidbody rigidbody;

    private void Awake() 
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void handleMovement() 
    {
                float speed = rigidbody.velocity.magnitude;
        if(Input.GetKey(KeyCode.A)) {
            rigidbody.position += Vector3.left * this.playerSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S)) {
            rigidbody.position += Vector3.back * this.playerSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D)) {
            rigidbody.position += Vector3.right * this.playerSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.W)) {
            rigidbody.position += Vector3.forward * this.playerSpeed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            rigidbody.velocity += Vector3.up * this.jumpStrength;
        }
    }
}
