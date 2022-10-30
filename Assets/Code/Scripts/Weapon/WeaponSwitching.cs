using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] private Transform[] weapons;

    [Header("keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("swich time")]
    [SerializeField] private float swichTime;

    private int selectWeapon;
    private float timeSinceLastSwich;

    private void Start() {
        setWeapons();
        select(selectWeapon); 
    }

    private void Update() {

        int previousSelectedWeapon = selectWeapon;
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwich > swichTime) {
                selectWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectWeapon) {
            select(selectWeapon);
        }

        timeSinceLastSwich += Time.deltaTime;
    }

    private void setWeapons() {
        weapons = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) {
            keys = new KeyCode[weapons.Length];
        }
    }

    private void select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        timeSinceLastSwich = 0;
        onWeaponSelected();
    }

    private void onWeaponSelected() {
        print("select new weapon");
    }
}
