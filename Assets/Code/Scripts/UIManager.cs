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
    public GameObject Flamethrower;
    private int currWeapon = 1;
    void Start() 
    {
        // init health here
    }
    void Update()
    {
        healthText.text = "Health: " + health;
        if(currWeapon == 1)
        {
            ammoText.text = "Ammo: " + Ak47.GetComponent<GunController>().GetClipSize() + " / " + Ak47.GetComponent<GunController>().GetBulletCapacity();
        }
        else if(currWeapon == 2)
        {
            ammoText.text = "Ammo: infinite";
        }

        WeaponChange();
        
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

    private void WeaponChange()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeapon = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeapon = 2;
        }
    }
}
