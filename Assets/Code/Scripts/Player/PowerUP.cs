using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    [SerializeField] private int powerUpTime;

    [SerializeField] public AudioSource pickUpSound;
    private int numOfJump = 0;
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(pickUp(other));
        }
    }

    IEnumerator pickUp(Collider player) {
        
        //power up effect
       
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        //apply power up to player
        var playerInput = player.GetComponent<PlayerInputs>();
        
        var AK47 = FindObjectOfType<GunController>();
        var playerCharacter = FindObjectOfType<PlayerCharacter>();
        
        //limit the number of "jumUp" powerups taken by the player at the same time
        if (gameObject.tag == "jumpUp" && numOfJump == 0) {
                FindObjectOfType<Timer>().Duration = powerUpTime;
                FindObjectOfType<Timer>().timerText.text = "Jump++";
                FindObjectOfType<Timer>().StartTimer();
                playerInput.jumpStrength *= (multiplier*0.8f);
                numOfJump++;
        } else if (gameObject.tag == "speedUp") { 
            FindObjectOfType<Timer>().Duration = powerUpTime;
            FindObjectOfType<Timer>().timerText.text = "Speed++";
            FindObjectOfType<Timer>().StartTimer();
            playerInput.playerSpeed *= multiplier;
        } else if (gameObject.tag == "amoUp") {
            if(AK47 != null){
                AK47.bulletTotal = AK47.bulletCapacity;
            }
            
        } else if (gameObject.tag == "rateUp") {
            FindObjectOfType<Timer>().Duration = powerUpTime;
            FindObjectOfType<Timer>().timerText.text = "FireRate++";
            FindObjectOfType<Timer>().StartTimer();
            if(AK47 != null){
                AK47.fireRate /= multiplier;
            }
        } else if (gameObject.tag == "healthUp") {
            playerCharacter.currentHealth += 1;
            playerCharacter.healthBar.SetHealth(playerCharacter.currentHealth);
        }

        
        pickUpSound.Play();
        
        
        //wait for some time, then go back to normal
        yield return new WaitForSeconds(powerUpTime);
        
        if (gameObject.tag == "speedUp") {
            playerInput.playerSpeed /= multiplier;
        } else if (gameObject.tag == "jumpUp") {
            playerInput.jumpStrength /= (multiplier*0.8f);
            numOfJump--;
        } else if (gameObject.tag == "rateUp") {
            if(AK47 != null){
                AK47.fireRate *= multiplier;
            }
        } 
        Destroy(gameObject);
    }
}
