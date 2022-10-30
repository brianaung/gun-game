using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private string tagToDamage;
    [SerializeField] private string Role;
    public AudioClip clip;

    private void Start() {
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            if(Role != "boss")
            {
                //AudioSource.PlayClipAtPoint(clip, transform.position);
                Destroy(gameObject);
            }
        }
    }

    public string getRole(){
        return this.Role;
    }
}
