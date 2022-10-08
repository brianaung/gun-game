using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {
    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    public int clipSize = 20;
    public int bulletCapacity = 300;

    [Header("Mouse Setting")]
    public float mouseSensitivity = 1;
    Vector2 currentRotation;

    //weapon Recoil
    public bool randomRecoil;
    public Vector2 randomRecoilConstraints;


    //variable
    bool canShoot;
    int currentBullet;
    int bulletTotal;

    //muzzel flash
    public Image muzzleFlashImage;
    public Sprite[] flashes;  

    private void Start() {
        canShoot = true;
        currentBullet = clipSize;
        bulletTotal = bulletCapacity;
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0,0,0,0);
    }
      
    private void Update() {
        gunMovement();
        if (Input.GetMouseButton(0) && canShoot && currentBullet>0) {
            canShoot = false;
            currentBullet--;
            StartCoroutine(shoot());
        } else if (Input.GetKeyDown(KeyCode.R) && currentBullet<clipSize && bulletTotal>0) {
            if (bulletTotal>=(clipSize-currentBullet)) {
                bulletTotal-=(clipSize-currentBullet); 
                currentBullet = clipSize;
            } else {
                currentBullet+=bulletTotal;
                bulletTotal=0;
            }
        }
    }    

    void gunMovement() {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        currentRotation += mouseAxis;

        currentRotation.y = Mathf.Clamp(currentRotation.y, -30, 45);

        // transform.root.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
        // transform.parent.localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);
    }

    IEnumerator shoot() {
        gunRecoil();
        StartCoroutine(muzzleFlash());
        RaycastToEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator muzzleFlash() {
        muzzleFlashImage.sprite = flashes[Random.Range(0,flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0,0,0,0);
    }

    void gunRecoil() {
        transform.root.localPosition -= Vector3.forward*100f;
        transform.root.localPosition += Vector3.forward*100f;
        if (randomRecoil) {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);
            currentRotation += recoil;
        }
    }
    
    //TODO: gun aiming effect
    void gunAim() {

    }

    void RaycastToEnemy() {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1<< LayerMask.NameToLayer("Enemy"))) {
            try {
                Debug.Log("Hit an enemy");
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.parent.transform.forward*500);
            } catch {

            }
        }
    }

    //TODO: swich weapon
    void swichWeapon() {
        
    }

    public int GetClipSize()
    {
        return currentBullet;
    }

    public int GetBulletCapacity()
    {
        return bulletTotal;
    }
}
