using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5;
    public float turnSpeed = 180;
    public float jumpPower = 5;

    private Rigidbody rb;
    private float yaw;
    private bool isJumpRequest;
    
    private Vector2 direction;

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update() {
        GetInput();
        Rotate();

        DebugRay();
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }
    
    private void Rotate() {
        yaw += direction.x * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, yaw, 0);
    }

    private void GetInput() {
        direction = new Vector2(
            Input.GetAxis("Horizontal") * turnSpeed
        ,   Input.GetAxis("Vertical") * speed
        );

        if (Input.GetButtonDown("Jump")) {
            isJumpRequest = true;
        }
    }

    private void Jump() {
        if (isJumpRequest && IsGrounded()) {
            rb.velocity += Vector3.up * jumpPower;
        }

        isJumpRequest = false;
    }

    private void Move() {
        Vector3 force = transform.forward * direction.y;
        force.y = rb.velocity.y;

        rb.velocity = force;
    }

    private void DebugRay() {
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * .2f);
    }

    private bool IsGrounded() {
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, .2f)) {
            return true;
        } else {
            return false;
        }
    }
}
