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
    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            playerInputs.handleMovement();
            playerInputs.handleRotation();
            cameraController.cameraMove();   
        }

        if(GameManager.Instance.gameOver)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.Instance.playAgain();
            }
        }

    }
}
