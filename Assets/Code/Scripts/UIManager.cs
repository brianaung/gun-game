using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    private int health = 5;
    private int ammo = 30;
    public TMP_Text healthText;
    public TMP_Text ammoText;

    public GameObject Ak47;
    void Start() 
    {
        // init health here
    }
    void Update()
    {
        healthText.text = "Health: " + health;
        ammoText.text = "Ammo: " + Ak47.GetComponent<GunController>().GetClipSize() + " / " + Ak47.GetComponent<GunController>().GetBulletCapacity();
        if(Input.GetKeyDown(KeyCode.H))
        {
            HealthDecrease();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            HealthIncrease();
        }
    }

    private void HealthDecrease()
    {
        health--;
    }

    private void HealthIncrease()
    {
        health++;
    }
}
