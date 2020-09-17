using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    public float speed = 5;
    public float mouseSensitivity = 2;
    public float jumpPower = 5;
    public bool invertX;
    public bool invertY;

    private Vector2 inputMove;
    private Vector2 inputTurn;
    private float yaw;
    private float pitch;
    private Rigidbody rb;
    private bool isRequestJump;


    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update() {
        GetInput();
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    private void LateUpdate() {
        Rotate();
    }

    private void GetInput() {
        inputMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        inputTurn = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        yaw += inputTurn.x * mouseSensitivity * (invertX ? -1 : 1);
        pitch -= inputTurn.y * mouseSensitivity * (invertY ? -1 : 1);
        pitch = Mathf.Clamp(pitch, -89, 55);


        if (Input.GetButtonDown("Jump")) {
            isRequestJump = true;
        }
    }

    private void Rotate() {
        transform.localEulerAngles = new Vector3(0, yaw, 0);

        Camera.main.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    private void Move() {
        Vector3 force = transform.right * speed * inputMove.x;
        force += transform.forward * speed * inputMove.y;
        force.y = rb.velocity.y;

        rb.velocity = force;
    }

    private void Jump() {
        if (isRequestJump && IsGrounded()) {
            rb.velocity += Vector3.up * jumpPower;
        }

        isRequestJump = false;
    }

    private bool IsGrounded() {
        RaycastHit hit;
        Vector3 origin = transform.position + (Vector3.up * 0.1f);
        float distance = 0.2f;
        Debug.DrawRay(origin, Vector3.down * distance);

        return Physics.Raycast(origin, Vector3.down, out hit, distance);
    }
}
