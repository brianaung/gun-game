

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileController : MonoBehaviour
{

     [SerializeField] private string tagToDamage;
     [SerializeField] private int damageAmount = 50;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if(col.gameObject.GetComponent<HealthManager>() != null)
            {
                var healthManager = col.gameObject.GetComponent<HealthManager>();
                healthManager.ApplyDamage(this.damageAmount);
                Destroy(gameObject);
            }
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
