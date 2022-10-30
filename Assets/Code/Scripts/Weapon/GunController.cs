using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {
    [Header("Gun Setting")]
    public float fireRate = 0.05f;
    public int clipSize = 20;
    public int bulletCapacity = 300;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletVelocity;
    [SerializeField] public AudioSource gunSound;
    [SerializeField] public AudioSource reloadSound;

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
    public Image muzzleFlashImage;
    public Sprite[] flashes;  
    private float timer;
    private void Start() {
        timer = fireRate;
        currentBullet = clipSize;
        bulletTotal = bulletCapacity;
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0,0,0,0);
    }
      
    private void Update() {
        gunMovement();
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && (timer >= fireRate) && currentBullet>0) {
            timer = 0;
            currentBullet--;
            StartCoroutine(shoot());
        } else if (Input.GetKeyDown(KeyCode.R) && currentBullet<clipSize && bulletTotal>0) {
            StartCoroutine(reload());
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
    }

    IEnumerator reload() {
        reloadSound.Play();
        
        yield return new WaitForSeconds(3);
        
    }

    IEnumerator shoot() {
        gunRecoil();
        StartCoroutine(muzzleFlash());
        
        var projectile = Instantiate(this.projectilePrefab, this.firePoint.position, this.firePoint.rotation);
        gunSound.Play();
            
        yield return null;
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
    

    void RaycastToEnemy() {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1<< LayerMask.NameToLayer("Enemy"))) {
            try {
                
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.parent.transform.forward*500);
            } catch {

            }
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
