using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject speedUp;
    [SerializeField] private GameObject jumpUp;
    [SerializeField] private GameObject amoUp;
    [SerializeField] private GameObject rateUp;
    [SerializeField] private GameObject healthUp;
    private MeshRenderer _renderer;
    public AudioClip clip;

    private void Awake()
    {
        this._renderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Same as above, but listens to onDeath events.
    public void Kill()
    {
        var particles = Instantiate(this.deathEffect);
        particles.transform.position = transform.position;
        AudioSource.PlayClipAtPoint(clip, transform.position);
        DropProp();
    }

    private void DropProp() {
        System.Random random = new System.Random();
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
        int num = random.Next(100); 
        if (num % 13 == 0) {
            var jumpUp = Instantiate(this.jumpUp);
            jumpUp.transform.position = position;
        } else if (num % 11 == 0) {
            var speedUp = Instantiate(this.speedUp);
            speedUp.transform.position = position;
        } else if (num % 5 == 0) {
            var amoUp = Instantiate(this.amoUp);
            amoUp.transform.position = position;
        } else if (num % 7 == 0) {
            var rateUp = Instantiate(this.rateUp);
            rateUp.transform.position = position;
        } else if (num % 3 == 0) {
            // higher chance of getting health powerup?
            var healthUp = Instantiate(this.healthUp);
            healthUp.transform.position = position;
        }
    }
}
