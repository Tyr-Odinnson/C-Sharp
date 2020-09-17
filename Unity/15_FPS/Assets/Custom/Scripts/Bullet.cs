using UnityEngine;

public class Bullet : MonoBehaviour {
    public float power = 20;
    public ParticleSystem explosion;

    public Rigidbody rb { get; set; }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Camera.main.transform.forward * power;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "KillBox" || collision.transform.tag == "Enemy") {
            Explode();
        }
    }

    public void Explode() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
