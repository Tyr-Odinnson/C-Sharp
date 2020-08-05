using UnityEngine;

public class MovingRigidbody : MonoBehaviour
{
    private float speed = 5;

    private Rigidbody rb;
    private float h;
    private float v;
    private bool isJumpRequest;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Jump")) {
            isJumpRequest = true;
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(h, rb.velocity.y, v);

        if (isJumpRequest) {
            rb.velocity += Vector3.up * 10;
        }
        
        isJumpRequest = false;
    }
}
