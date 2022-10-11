using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour
{
    public GameObject Ak47;
    public GameObject Flamethrower;
    public GameObject Player;
    private float sensitivity = 1.5f;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    void Update()
    {
        var MouseX = Input.GetAxis("Mouse X") * sensitivity;
        var MouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += MouseX;
        rotationY += MouseY;
        rotationZ += MouseX;
        rotationZ += MouseY;
        
        Player.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        Ak47.transform.localRotation = Quaternion.Euler(rotationZ, rotationX, rotationY);
        Flamethrower.transform.localRotation = Quaternion.Euler(rotationY, rotationZ, rotationX);
    }

}
