using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraCenter;
    public float yOffset = 1.0f; // so camera matches with head of player
    public float zOffset = -10.0f;
    public float sensitivity = 1.0f;
    private Transform playerTransform;
    private void Awake() 
    {
        playerTransform = player.transform;
    }
    void Update() 
    {
         

        // cameraCenter.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z - zOffset);

        // camera rotation from https://www.youtube.com/watch?v=cAh--AfQWVw
        Quaternion rotation = Quaternion.Euler(cameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity, cameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, cameraCenter.transform.rotation.eulerAngles.z);

        cameraCenter.transform.rotation = rotation; 
    }

    public void followPlayer() 
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
    }
}


