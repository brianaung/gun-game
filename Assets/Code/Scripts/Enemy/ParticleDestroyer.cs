using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem targetParticleSystem;

    private void Update(){
        if(!this.targetParticleSystem.IsAlive())
            Destroy(this.targetParticleSystem.gameObject);
    }
}
