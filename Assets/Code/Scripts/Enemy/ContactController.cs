using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private int damageAmount = 50;
    [SerializeField] private string tagToDamage;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            // Damage object with relevant tag. Note that this assumes the 
            // HealthManager component is attached to the respective object.
            // var healthManager = col.gameObject.GetComponent<HealthManager>();
            // healthManager.ApplyDamage(this.damageAmount);
           
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            
            // Destroy self.
            Destroy(gameObject);
        }
    }
}
