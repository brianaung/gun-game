using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject scaleUp;
    [SerializeField] private GameObject jumpUp;
    [SerializeField] private GameObject amoUp;
    [SerializeField] private GameObject rateUp;
    [SerializeField] private GameObject healthUp;
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
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
        int num = random.Next(10); 
        if (num<=1) {
            var jumpUp = Instantiate(this.jumpUp);
            jumpUp.transform.position = position;
        } else if (num<=1) {
            var scaleUp = Instantiate(this.scaleUp);
            scaleUp.transform.position = position;
        } else if (num<=1) {
            var amoUp = Instantiate(this.amoUp);
            amoUp.transform.position = position;
        } else if (num<=8) {
            var rateUp = Instantiate(this.rateUp);
            rateUp.transform.position = position;
        } else if (num<=10) {
            // higher chance of getting health powerup?
            var healthUp = Instantiate(this.healthUp);
            healthUp.transform.position = position;
        }
    }
}
