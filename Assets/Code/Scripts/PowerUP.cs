using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField] private int multiplier = 2;
    //[SerializeField] private ParticleSystem powerUpEffect;
    [SerializeField] private int powerUpTime;
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(pickUp(other));
        }
    }

    IEnumerator pickUp(Collider player) {
        
        //power up effect
        //Instantiate(this.powerUpEffect, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        //apply power up to player
        var playerInput = player.GetComponent<PlayerInputs>();
        
        var AK47 = FindObjectOfType<GunController>();
        //var FireThrower = FindObjectOfType<FireThrowerController>();
        if (gameObject.tag == "speedUp") {
            playerInput.playerSpeed *= multiplier;
        } else if (gameObject.tag == "jumpUp") {
            playerInput.jumpStrength *= multiplier;
        } else if (gameObject.tag == "scaleUp") {
            player.transform.localScale *= multiplier;
        } else if (gameObject.tag == "amoUp") {
            AK47.bulletTotal = AK47.bulletCapacity;
            //FireThrower.bulletTotal = 100;
        }
        
        
        
        //wait for some time, then go back to normal
        yield return new WaitForSeconds(powerUpTime);
        //player.transform.localScale /= multiplier;
        //playerInput.jumpStrength /= multiplier;
        if (gameObject.tag == "speedUp") {
            playerInput.playerSpeed /= multiplier;
        } else if (gameObject.tag == "jumpUp") {
            playerInput.jumpStrength /= multiplier;
        } else if (gameObject.tag == "scaleUp") {
            player.transform.localScale /= multiplier;
        }
        Destroy(gameObject);
        
        
    }
}


