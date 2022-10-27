using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputs playerInputs;
    public CameraController cameraController;
    
    private void Awake() 
    {
        playerInputs = GetComponent<PlayerInputs>();
    }
    void Update()
    {
        if(!GameManager.Instance.gameOver && !GameManager.Instance.gameWin)
        {
            playerInputs.handleMovement();
            playerInputs.handleRotation();
            cameraController.cameraMove();   
        }

        if(GameManager.Instance.gameOver || GameManager.Instance.gameWin)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.Instance.playAgain();
                Cursor.lockState = CursorLockMode.None;
            }
        }

    }
}
