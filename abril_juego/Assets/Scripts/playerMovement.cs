using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float runSpeed = 5f;
    public float jumpForce = 100f;
    float moveInput;
    Rigidbody2D playerRB;

    bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        moveInput = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(moveInput * runSpeed, playerRB.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0) {
            Flip();
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    void Jump() {
        if (isGrounded == true) {
            playerRB.velocity = Vector2.up * jumpForce;
        }       
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"){
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
