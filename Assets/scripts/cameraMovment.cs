using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovment : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraCenter;
    public Camera cam;
    public float yOffset = 1.0f; // so camera matches with head of player
    public float zOffset = -13.0f;

    public float sensitivity = 1.0f;

    void Update() 
    {
        // camera rotation from https://www.youtube.com/watch?v=cAh--AfQWVw
        cameraCenter.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z - zOffset);

        Quaternion rotation = Quaternion.Euler(cameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity, cameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, cameraCenter.transform.rotation.eulerAngles.z);

        cameraCenter.transform.rotation = rotation; 
    }
}


