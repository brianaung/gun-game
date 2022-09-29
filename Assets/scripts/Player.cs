using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInputs playerInputs;
    public CameraController cameraController;

    private void Awake() 
    {
        playerInputs = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     playerInputs.handleMovement();
    //     playerInputs.handleRotation();
    // }

    private void LateUpdate() 
    {
        cameraController.followPlayer();
    }
}
