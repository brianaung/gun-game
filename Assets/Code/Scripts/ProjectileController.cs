

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.tag == this.tagToDamage)
        //{
            // Damage object with relevant tag. Note that this assumes the 
            // HealthManager component is attached to the respective object.
            //var healthManager = col.gameObject.GetComponent<HealthManager>();
            //healthManager.ApplyDamage(this.damageAmount);
            
            // Create collision particles in opposite direction to movement.
            // var particles = Instantiate(this.collisionParticles);
            // particles.transform.position = transform.position;
            // particles.transform.rotation =
            //     Quaternion.LookRotation(-this.velocity);

            // Destroy self.
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameManager.Instance.updateKills();
        //}
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
