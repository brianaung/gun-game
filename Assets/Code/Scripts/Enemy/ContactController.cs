using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private string tagToDamage;
    // [SerializeField] private int damageAmount = 50;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            
            Destroy(gameObject);
        }
    }
}
