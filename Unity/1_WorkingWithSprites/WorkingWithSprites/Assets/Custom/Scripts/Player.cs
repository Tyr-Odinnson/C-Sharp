using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 8.0f;
    public float jumpPower = 10.0f;
    public float groundCheckDistance = 1.1f;
    [Range(0, 1)]
    public float extraGravity = 0.2f;

    private float h;
    private bool isJumpRequest;
    private bool isGrounded;
    private SpriteRenderer spr;
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake() {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update() {
        h = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButton("Horizontal")) {
            Debug.Log(h);
            spr.flipX = Input.GetAxisRaw("Horizontal") < 0;
        }

        if (Input.GetButtonDown("Jump")) {
            isJumpRequest = true;
        }

        UpdateAnimator();
    }

    void FixedUpdate() {
        isGrounded = GetIsGrounded();

        Move();
        Jump();
    }

    private void Jump() {
        if (isGrounded && isJumpRequest) {
            rb.velocity += Vector2.up * jumpPower;
        }
        isJumpRequest = false;

        if (!isGrounded) {
            rb.velocity += Vector2.down * extraGravity;
        }
    }

    private void Move() {
        rb.velocity = new Vector2(h, rb.velocity.y);
    }

    private void UpdateAnimator() {
        anim.SetFloat("Horizontal", Mathf.Abs(h));
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetBool("IsGrounded", isGrounded);
    }

    private bool GetIsGrounded() {
        Vector2 origin = col.bounds.center;

        // Remember to uncheck queriesStartInColliders.
        RaycastHit2D hit = Physics2D.Raycast(
            origin
        ,   Vector2.down
        ,   groundCheckDistance
        );

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.red);
        
        return hit;
    }
}
