﻿using UnityEngine;

public class ControllerThirdPersonSimple : MonoBehaviour {
    public float speed = 5;
    public float jumpPower = 5;

    private Rigidbody rb;
    private float h;
    private float v;
    private bool isJumpRequest;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update() {
        GetInputs();
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    void GetInputs() {
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Jump")) {
            isJumpRequest = true;
        }
    }

    void Move() {
        rb.velocity = new Vector3(h, rb.velocity.y, v);
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