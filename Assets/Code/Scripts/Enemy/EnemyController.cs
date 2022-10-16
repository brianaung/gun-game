using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject speedUp;
    [SerializeField] private GameObject scaleUp;
    [SerializeField] private GameObject jumpUp;
    [SerializeField] private GameObject amoUp;
    
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
        int num = random.Next(10); 
        if (num<=2) {
            var speedUp = Instantiate(this.speedUp);
            speedUp.transform.position = transform.position;
        } else if (num<=4) {
            var jumpUp = Instantiate(this.jumpUp);
            jumpUp.transform.position = transform.position;
        } else if (num<=6) {
            var scaleUp = Instantiate(this.scaleUp);
            scaleUp.transform.position = transform.position;
        } else if (num<=10) {
            var amoUp = Instantiate(this.amoUp);
            amoUp.transform.position = transform.position;
        }
    }
}
