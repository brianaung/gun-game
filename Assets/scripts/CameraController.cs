using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraCenter;
    public float yOffset = 1.0f; // so camera matches with head of player
    public float zOffset = -10.0f;

    private Vector3 offset;
    public float sensitivity = 1.0f;
    private Transform playerTransform;
    private void Awake() 
    {
        playerTransform = player.transform;
    }
    
    private void Start()
    {
        
    }
    void Update() 
    {

        // camera rotation from https://www.youtube.com/watch?v=cAh--AfQWVw
        // Quaternion rotation = Quaternion.Euler(cameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity, cameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, cameraCenter.transform.rotation.eulerAngles.z);

        // cameraCenter.transform.rotation = rotation; 


    }

    private void LateUpdate() 
    {
        followPlayer();
    }

    public void followPlayer() 
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
        transform.LookAt(playerTransform.position);
    }
}


