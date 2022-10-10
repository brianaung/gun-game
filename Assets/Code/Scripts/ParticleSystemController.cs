using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void OnParticleCollision(GameObject enemy) {
        Destroy(enemy.gameObject);
    }

}
