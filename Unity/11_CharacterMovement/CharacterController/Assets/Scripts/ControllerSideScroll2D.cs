using UnityEngine;

public class ControllerSideScroll2D : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 5;

    private Rigidbody2D rb;
    private float h;
    private bool isJumpRequest;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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

    void GetInputs()
    {
        h = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumpRequest = true;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(h, rb.velocity.y);
    }

    void Jump()
    {
        if (isJumpRequest && IsGrounded())
        {
            rb.velocity += Vector2.up * jumpPower;
        }

        isJumpRequest = false;
    }

    bool IsGrounded()
    {
        Vector2 origin = (Vector2)transform.position + (Vector2.up * 0.1f);
        float distance = 0.2f;
        Debug.DrawRay(origin, Vector2.down * distance);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance);

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}