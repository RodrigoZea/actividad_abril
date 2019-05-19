using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{

    public float runSpeed = 5f;
    public float jumpForce = 100f;
    public int playerScore = 0;
    float moveInput;

    bool beingHurt;
    float hurtTimer = 0f;

    Rigidbody2D playerRB;
    SpriteRenderer playerSR;
    AudioSource playerAS;

    public Sprite normalSprite;
    public Sprite jumpSprite;

    bool facingRight = true;
    BoxCollider2D playerBCD;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;

    public int health = 2;
    public Text healthText;
    public Text scoreText;
    public GameObject losePanel;

    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAS = GetComponent<AudioSource>();
        playerBCD = GetComponent<BoxCollider2D>();
        startPos = transform.position;
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

        if (isGrounded == true)
        {
            playerSR.sprite = normalSprite;
        }
        else {
            playerSR.sprite = jumpSprite;
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

        if (beingHurt == true){
            hurtTimer += Time.deltaTime;

            playerSR.color = Color.red;

            if (hurtTimer > 2f) {
                playerSR.color = Color.white;
                beingHurt = false;
                hurtTimer = 0;
            }
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

    void Eat() {
        if (health >= 2)
        {
            Vector3 scale = transform.localScale;
            playerAS.Play();

            if (facingRight == true)
            {
                scale.x += 0.15f;
            }
            else if (facingRight == false)
            {
                scale.x -= 0.15f;
            }

            jumpForce -= 0.3f;
            scale.y += 0.15f;
            runSpeed -= 0.5f;
            transform.localScale = scale;
            health += 1;
            healthText.text = health.ToString();
        }
        else {
            health += 1;
            healthText.text = health.ToString();
        }

    }

    void Hurt()
    {
        if (health > 2)
        {
            Vector3 scale = transform.localScale;

            if (facingRight == true)
            {
                scale.x -= 0.15f;
            }
            else if (facingRight == false)
            {
                scale.x += 0.15f;
            }


            jumpForce += 0.3f;
            scale.y -= 0.15f;
            runSpeed += 0.5f;
            transform.localScale = scale;
            health -= 1;
            healthText.text = health.ToString();
            checkGameOver();
            beingHurt = true;
        }
        else {
            health -= 1;
            healthText.text = health.ToString();
            checkGameOver();
            beingHurt = true;
        }

    }

    void checkGameOver() {
        if (health <= 0)
        {
            Debug.Log("You lose!");
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }

    void spawnToStart() {
        transform.position = startPos;
    }

    void ScoreAdd(int n) {
        playerScore += n;
        scoreText.text = playerScore.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"){
            isGrounded = true;
            
        }
        if (collision.collider.tag == "Enemy" && beingHurt == false)
        {
            Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            Eat();
        }
        if (collision.tag == "Coins")
        {
            Destroy(collision.gameObject);
            ScoreAdd(1);
        }
        if (collision.tag == "Door" && health > 2)
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Portals")
        {
            //Destroy(collision.gameObject);
            spawnToStart();
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
