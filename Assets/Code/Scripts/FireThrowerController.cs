using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class FireThrowerController : MonoBehaviour {
    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    public int clipSize = 20;
    public int bulletCapacity = 300;

    [SerializeField] private ParticleSystem flame;
    [SerializeField] private Light light;

    [Header("Mouse Setting")]
    public float mouseSensitivity = 1;
    Vector2 currentRotation;

    //weapon Recoil
    public bool randomRecoil;
    public Vector2 randomRecoilConstraints;


    //variable
    bool canShoot;
    int currentBullet;
    public int bulletTotal;

    //muzzel flash
    //public Image muzzleFlashImage;
    //public Sprite[] flashes;  

    private void Start() {
        flame.Stop();
        canShoot = true;
        currentBullet = clipSize;
        bulletTotal = bulletCapacity;
        //muzzleFlashImage.sprite = null;
        //muzzleFlashImage.color = new Color(0,0,0,0);
        
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
        } else if (Input.GetMouseButtonUp(0) || currentBullet<=0) {
            flame.Stop();
        }
    }    

    void gunMovement() {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        currentRotation += mouseAxis;

        currentRotation.y = Mathf.Clamp(currentRotation.y, -30, 45);

    }

    IEnumerator shoot() {
        gunRecoil();
        flame.Play();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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
    
    

    public int GetClipSize()
    {
        return currentBullet;
    }

    public int GetBulletCapacity()
    {
        return bulletTotal;
    }

    

}
