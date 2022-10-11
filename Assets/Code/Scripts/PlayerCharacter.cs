using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("playerSetting")]
    public int PlayerHealth;
    public Behaviour BlurScript;

    bool isShot;
    int currentHealth;
    bool start;
    
    // Start is called before the first frame update
    private void Start() {
        isShot = false;
        BlurScript.enabled = false;
        currentHealth = PlayerHealth;
    }

    private void OnTriggerEnter(Collider col) {
        currentHealth--;
        isShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShot) {
            BlurScript.enabled = true;
            StartCoroutine(Blur());
        }
    }

    IEnumerator Blur() {
        yield return new WaitForSeconds(2);
        isShot = false;
        BlurScript.enabled = false;
    }
}
