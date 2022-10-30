using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particle;
    [SerializeField] private int damageAmount = 10;

    private void OnParticleCollision(GameObject enemy) {
        if (enemy.CompareTag("Enemy")) {
            var healthManager = enemy.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(this.damageAmount);
        }
        
    }

}
