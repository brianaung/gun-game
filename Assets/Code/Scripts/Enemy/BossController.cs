using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshRenderer))]
public class BossController : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    private MeshRenderer _renderer;

    private void Awake()
    {
        this._renderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Same as above, but listens to onDeath events.
    public void Kill()
    {
        var particles = Instantiate(this.deathEffect);
        particles.transform.position = -transform.position;
        Destroy(gameObject);
        GameManager.Instance.winGame();
    }
}
