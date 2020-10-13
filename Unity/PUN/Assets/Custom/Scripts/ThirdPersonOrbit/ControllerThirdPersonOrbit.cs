using Photon.Pun;
using UnityEngine;

public class ControllerThirdPersonOrbit : MonoBehaviourPun {
    public float speedMotion = 5;
    public float speedRotation = 5;
    public float jumpPower = 5;

    private float h;
    private float v;
    private bool isJumpRequest;
        
    protected Rigidbody rb;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        FindObjectOfType<CameraOrbit>()?.SetTarget(transform);
    }

    protected virtual void Update() {
        GetInputs();
        Rotate();
    }

    protected virtual void FixedUpdate() {
        Move();
        Jump();
    }

    void GetInputs() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump")) {
            isJumpRequest = true;
        }
    }

    private void Move() {
        Vector3 forward = CameraOrbit.Instance.transform.forward * v;
        Vector3 right = CameraOrbit.Instance.transform.right * h;
        Vector3 force = (forward + right) * speedMotion;
        force.y = rb.velocity.y;

        rb.velocity = force;
    }

    private void Rotate() {
        if (new Vector2(h, v).magnitude > 0) {
            float targetRotation = (Mathf.Atan2(h, v) * Mathf.Rad2Deg) + CameraOrbit.Instance.yaw;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), speedRotation);
        }
    }

    void Jump() {
        if (isJumpRequest && IsGrounded()) {
            rb.velocity += Vector3.up * jumpPower;
        }

        isJumpRequest = false;
    }

    bool IsGrounded() {
        Vector3 origin = transform.position + (Vector3.up * 0.1f);
        float distance = 0.2f;
        Debug.DrawRay(origin, Vector3.down * distance);

        if (Physics.Raycast(origin, Vector3.down, distance)) {
            return true;
        } else {
            return false;
        }
    }
}
