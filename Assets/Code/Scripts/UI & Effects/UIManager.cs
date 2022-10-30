using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text ammoText;
    public TMP_Text killText;
    public TMP_Text endGameText;
    public GameObject Ak47;
    public GameObject Flamethrower;
    public GameObject player;
    private int currWeapon = 1;
    void Start() 
    {
        // init health here
    }
    void Update()
    {
        if(GameManager.Instance.gameOver)
        {
            endGameText.text = "You Died! D: \n Press enter to return to the main menu";
        }

        else if(GameManager.Instance.gameWin)
        {
            endGameText.text = "You Win! :D \n Press enter to return to the main menu";
        }

        else
        {
            endGameText.text = "";
        }

        
        killText.text = "Kills: " + GameManager.Instance.playerKills;
        if(currWeapon == 1)
        {
            ammoText.text = "Ammo: " + Ak47.GetComponent<GunController>().GetClipSize() + " / " + Ak47.GetComponent<GunController>().GetBulletCapacity();
        }
        else if(currWeapon == 2)
        {
            ammoText.text = "Ammo: " + Flamethrower.GetComponent<FireThrowerController>().GetClipSize() + " / " + Flamethrower.GetComponent<FireThrowerController>().GetBulletCapacity();
        }

        WeaponChange();
    
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
