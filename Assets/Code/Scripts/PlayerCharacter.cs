using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("playerSetting")]
    public int PlayerHealth;
    public Behaviour BlurScript;
    
    bool isShot;
    public int currentHealth;
    bool start;
    
    // Start is called before the first frame update
    private void Awake() {
        isShot = false;
        BlurScript.enabled = false;
        currentHealth = PlayerHealth;
    }

    private void OnTriggerEnter(Collider col) {
        if (currentHealth > 0) {
            currentHealth--;
        }
        
        isShot = true;
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

    IEnumerator Blur() {
        yield return new WaitForSeconds(2);
        isShot = false;
        BlurScript.enabled = false;
    }
}
