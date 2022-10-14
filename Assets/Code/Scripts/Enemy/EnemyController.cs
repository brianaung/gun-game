using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject prop;
    
    private MeshRenderer _renderer;

    private void Awake()
    {
        this._renderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Same as above, but listens to onDeath events.
    public void Kill()
    {
        var particles = Instantiate(this.deathEffect);
        particles.transform.position = transform.position;
        DropProp();
    }

    private void DropProp() {
        System.Random random = new System.Random(); 
        int num = random.Next(50); 
        if (num<=10) {
            var porpPowerUp = Instantiate(this.prop);
            porpPowerUp.transform.position = transform.position;
        }
    }
}
