using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossfireController : MonoBehaviour
{
    public ParticleSystem bossfire;
    public AudioClip clip;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] public AudioSource throwRockSounds;

    bool canShoot;
    private void Start() {
        bossfire.Stop();
        canShoot = true;
    }

    private void Update() {
        if (canShoot) {
            StartCoroutine(shoot());
            bossfire.Play();
            throwRockSounds.Play();
        } else {
            bossfire.Stop();
            //throwRockSounds.Stop();
        }

    }

    IEnumerator shoot() {
        canShoot = false;
        yield return new WaitForSeconds(10);
        canShoot = true;
    }
    private void OnParticleCollision(GameObject enemy) {
        if (enemy.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            this.onHit.Invoke();

        }
    }

}
