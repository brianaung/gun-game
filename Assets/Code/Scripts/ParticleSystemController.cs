using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    [SerializeField] private int damageAmount = 80;

    private void OnParticleCollision(GameObject enemy) {
        var healthManager = enemy.gameObject.GetComponent<HealthManager>();
        healthManager.ApplyDamage(this.damageAmount);
    }

}
