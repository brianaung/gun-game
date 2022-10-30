using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class FireThrowerController : MonoBehaviour {
    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    public int clipSize = 5;
    public int bulletCapacity = 50;

    [SerializeField] private ParticleSystem flame;
    [SerializeField] private Light LightSource;
    [SerializeField] public AudioSource FlameSounds;
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

    private float timer;

    
    private void Start() {
        flame.Stop();
        timer = fireRate;
        currentBullet = clipSize;
        bulletTotal = bulletCapacity;
        
        
    }
      
    private void Update() {
        gunMovement();
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && (timer > fireRate) && currentBullet>0) {
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
        } else if (Input.GetMouseButtonUp(0) || currentBullet<=0) {
            flame.Stop();
            FlameSounds.Stop();
        }
    }    

    void gunMovement() {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        currentRotation += mouseAxis;

        currentRotation.y = Mathf.Clamp(currentRotation.y, -30, 45);

    }

    IEnumerator shoot() {
        flame.Play();
        FlameSounds.Play();
        yield return null;
    }

    IEnumerator reload() {
        reloadSound.Play();
        yield return new WaitForSeconds(5);
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
