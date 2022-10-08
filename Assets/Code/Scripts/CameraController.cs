using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject camCenter;
    public Camera cam;
    public float yOffset = 1.0f;
    public float zOffset = -5.0f;
    public float sensitivity = 1.0f;
    private Transform playerTransform;
    private float rotationX;
    private float rotationY;
    private Vector3 currRotation;
    private Vector3 camVelocity = Vector3.zero;
    private float smoothTime = 0.1f;

    private float cameraCollisionOffset = 0.1f;
    private void Awake() 
    {
        playerTransform = player.transform;
    }

    public void cameraMove()
    {
        // camera movement help from https://www.youtube.com/watch?v=zVX9-c_aZVg
        float MouseX = Input.GetAxis("Mouse X") * sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += MouseX; 
        rotationY += MouseY;

        rotationY = Mathf.Clamp(rotationY, -40, 40);

        Vector3 nextRotation = new Vector3(-rotationY, rotationX);
        currRotation = Vector3.SmoothDamp(currRotation, nextRotation, ref camVelocity, smoothTime);

        transform.localEulerAngles = currRotation;
        transform.position = playerTransform.position - (transform.forward * -zOffset) + (transform.up * yOffset);

        // Basic camera collision. Simply checks if there is anything between the player and the camera. 
        // if there is, the camera will move to the hit point of the linecast between the player and camera

        if(Physics.Linecast(playerTransform.position, transform.position, out var hit))
        {
            transform.position = hit.point + new Vector3(0, 0, cameraCollisionOffset);
        }

    }
}


