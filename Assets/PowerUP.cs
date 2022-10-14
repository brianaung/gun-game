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
        player.transform.localScale *= multiplier;
        playerInput.jumpStrength *= multiplier;
        
        //wait for some time, then go back to normal
        yield return new WaitForSeconds(powerUpTime);
        player.transform.localScale /= multiplier;
        playerInput.jumpStrength /= multiplier;
        Destroy(gameObject);
        
        
    }
}


