using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("playerSetting")]
    public int PlayerHealth;
    public Behaviour BlurScript;
    public HealthBar healthBar;
    
    bool isShot;
    public int currentHealth;
    [SerializeField] private string enemyTag;
    bool start;
    
    // Start is called before the first frame update
    private void Awake() {
        isShot = false;
        BlurScript.enabled = false;
        currentHealth = PlayerHealth;
        healthBar.SetMaxHealth(PlayerHealth);
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == this.enemyTag){
            this.ApplyDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShot) {
            BlurScript.enabled = true;
            StartCoroutine(Blur());
        }
        
        if(currentHealth <= 0)
        {
            BlurScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.endGame();
        }
    }

    public void ApplyDamage(){
        if (currentHealth > 0) {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
        isShot = true;
    }

    IEnumerator Blur() {
        yield return new WaitForSeconds(2);
        isShot = false;
        BlurScript.enabled = false;
    }
}
