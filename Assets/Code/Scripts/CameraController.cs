using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
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
    private float cameraCollisionOffset = 2.5f;
    private float maxDist = 4f;
    private float playerCamDist;
    private void Awake() 
    {
        playerTransform = player.transform;
        transform.position = playerTransform.position - (transform.forward * -zOffset) + (transform.up * yOffset);
        playerCamDist = Vector3.Distance(playerTransform.position, transform.position);
        
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

        

        // transform.position = playerTransform.position - (transform.forward * -zOffset) + (transform.up * yOffset);

        // Basic camera collision. Checks if there is object behind the camera. if there is set the transform of the camera to that hit point minus 
        // some offset. There is another if statement to check if the player is too far away from the camera as the camera will get stuck to the wall.
        // Although this method works, it will sometimes still clip through a wall when moving the camera at some weird angles.
        if(Physics.Raycast(transform.position, -transform.forward, out var hit, maxDist))
        {
            transform.position = hit.point - (transform.forward * -cameraCollisionOffset);
            if(Vector3.Distance(transform.position, playerTransform.position) > playerCamDist)
            {
                transform.position = playerTransform.position - (transform.forward * -zOffset) + (transform.up * yOffset);
            }
        }

        else
        {
            transform.position = playerTransform.position - (transform.forward * -zOffset) + (transform.up * yOffset);
        }
        
    }
}


