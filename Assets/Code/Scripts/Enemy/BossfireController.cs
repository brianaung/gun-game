using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossfireController : MonoBehaviour
{
    public ParticleSystem bossfire;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private UnityEvent onHit;

    bool canShoot;
    private void Start() {
        bossfire.Stop();
        canShoot = true;
    }

    private void Update() {
        //StartCoroutine(wait());
        
        if (canShoot) {
            StartCoroutine(shoot());
            bossfire.Play();
        } else {
            bossfire.Stop();
        }

    }

    IEnumerator shoot() {
        //bossfire.Play();
        canShoot = false;
        yield return new WaitForSeconds(10);
        canShoot = true;
    }

    // IEnumerator wait() {
    //     yield return new WaitForSeconds(8);
    // }
    

    private void OnParticleCollision(GameObject enemy) {
        if (enemy.CompareTag("Player")) {
            this.onHit.Invoke();
        }
    }

}
