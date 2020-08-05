using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyBall : MonoBehaviour {

    public float speed = 10;

    private Rigidbody rb;
    private Vector2 myInput;

    // private float h;
    // private float v;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // h = Input.GetAxis("Horizontal");
        // v = Input.GetAxis("Vertical");

        myInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(myInput.x * speed, rb.velocity.y, myInput.y * speed);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }

}
