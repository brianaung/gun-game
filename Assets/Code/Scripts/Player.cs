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
<<<<<<< HEAD
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
=======
        playerInputs.handleMovement();
        playerInputs.handleRotation();
        
    }
>>>>>>> 08baf05156f2bf3770e2951c75dec24e7e9586da

    }
}
