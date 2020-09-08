using System;
using UnityEngine;

public class ControllerTank : MonoBehaviour
{
    public float speed = 5;
    public float turnSpeed = 3;
    public float jumpPower = 5;

    private Rigidbody rb;
    private float h;
    private float v;
    private bool isJumpRequest;
    private float yaw;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        GetInputs();
        Rotate();
    }


    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 force = transform.forward * v;
        force.y = rb.velocity.y;
        rb.velocity = force;
    }

    private void Rotate()
    {
        yaw += h;
        transform.localEulerAngles = new Vector3(0, yaw, 0);
    }

    void GetInputs()
    {
        h = Input.GetAxis("Horizontal") * turnSpeed;
        v = Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumpRequest = true;
        }
    }

    void Jump()
    {
        if (isJumpRequest && IsGrounded())
        {
            rb.velocity += Vector3.up * jumpPower;
        }

        isJumpRequest = false;
    }

    bool IsGrounded()
    {
        Vector3 origin = transform.position + (Vector3.up * 0.1f);
        float distance = 0.2f;
        Debug.DrawRay(origin, Vector3.down * distance);

        if (Physics.Raycast(origin, Vector3.down, distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
