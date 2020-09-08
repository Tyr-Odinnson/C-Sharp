using UnityEngine;

public class ControllerFirstPerson : MonoBehaviour
{
    public float speed = 5;
    public float mouseSensitivity = 2;
    public float jumpPower = 5;

    private Rigidbody rb;
    private float h;
    private float v;
    private float mouseH;
    private float mouseV;
    private bool isJumpRequest;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void LateUpdate()
    {
        Turn();
    }

    private void Turn()
    {
        transform.localEulerAngles += Vector3.up * mouseH * mouseSensitivity;
        Camera.main.transform.localEulerAngles -= Vector3.right * mouseV * mouseSensitivity;
    }

    private void Move()
    {
        Vector3 force = transform.forward * speed * v;
        force += transform.right * speed * h;
        force.y = rb.velocity.y;

        rb.velocity = force;
    }

    void GetInputs()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        mouseH = Input.GetAxis("Mouse X");
        mouseV = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Jump"))
        {
            isJumpRequest = true;
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
