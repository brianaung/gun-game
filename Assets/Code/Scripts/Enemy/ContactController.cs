using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    // [SerializeField] private int damageAmount = 50;
    [SerializeField] private string tagToDamage;
    [SerializeField] private string Role;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            
            if(Role != "boss") Destroy(gameObject);
        }
    }
}
