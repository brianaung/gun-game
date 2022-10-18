

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    //  [SerializeField] private ParticleSystem collisionParticles;
     [SerializeField] private string tagToDamage;
     [SerializeField] private int damageAmount = 50;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            // var particles = Instantiate(this.collisionParticles);
            // particles.transform.position = transform.position;
            // particles.transform.rotation =
            //     Quaternion.LookRotation(-this.velocity);

            var healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(this.damageAmount);
            Destroy(gameObject);
        }
    }

    private void Start() {
        StartCoroutine(wait());
    }

     IEnumerator wait() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void Update() {
        transform.Translate(Vector3.forward*Time.deltaTime*30);
    }

    
}
