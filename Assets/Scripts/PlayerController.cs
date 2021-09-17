using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Status")]
    public string playerID;
    public HealthBar healthBar;
    public int fullHealth;
    public int health;
    public float speed;
    public float jumpForce;
    public bool isDead;

    [Header("Key Binding")]
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode crouch;

    [Header("Key Bound")]
    public float lowerBound;


    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private AudioManager audioManager;

    private float horizontalInput;

    private bool isGrounded = true;
    private bool isCrouch = false;

    private bool isMovingRight = false;
    private bool isMovingLeft = false;

    private bool isFacingRight = true;
    private bool isFacingLeft = false;


    private void Awake() 
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        health = fullHealth;
        healthBar.SetMaxHealth(fullHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // ignore side collide
        if(collision.contacts.Length > 1) {
            if(collision.contacts[0].point.x == collision.contacts[1].point.x) return;
        }

        string type = collision.collider.tag.Substring(0,5);
        if(type == "Stair" && !collision.collider.CompareTag("Stair_Bounce")) {
            isGrounded = true;
            playerAnim.SetBool("IsGrounded", true);
        }

        if(collision.collider.CompareTag("Stair_Bounce")) {
            collision.collider.GetComponent<BounceStair>().BounceStairCollision(playerRb);
        }
        if(collision.collider.CompareTag("Stair_Faded")) {
            collision.collider.GetComponent<FadedStair>().FadedStairCollision();
        }
        if(collision.collider.CompareTag("Stair_PushLeft")) {
            // You can add another function here
        }
        if(collision.collider.CompareTag("Stair_PushRight")) {
            // You can add another function here
        }
        if(collision.collider.CompareTag("Stair_Fire")) {
            health -= collision.collider.gameObject.GetComponent<FireStair>().GetDamage();
            healthBar.SetHealth(health);

            playerAnim.SetTrigger("Hurt");
            audioManager.Play("Hurt");
        }

    }


    private void OnCollisionExit2D(Collision2D collision) 
    {
        // ignore side collide
        if(collision.contacts.Length > 1) {
            if(collision.contacts[0].point.x == collision.contacts[1].point.x) return;
        }

        string type = collision.collider.tag.Substring(0,5);
        if(type == "Stair") {
            isGrounded = false;
            playerAnim.SetBool("IsGrounded", false);
        }
    }


    private void Update()
    {
        // jump
        if(Input.GetKeyDown(jump) && isGrounded) {
            if(isCrouch) playerRb.AddForce(Vector2.up * jumpForce * 0.65f);
            else playerRb.AddForce(Vector2.up * jumpForce);

            audioManager.Play("Jump");            
        }

        // crouch
        if(Input.GetKeyDown(crouch) && isGrounded) {
            isCrouch = true;
            playerAnim.SetBool("IsCrouch", true);
        }
        if(Input.GetKeyUp(crouch) || !isGrounded) {
            isCrouch = false;
            playerAnim.SetBool("IsCrouch", false);
        }


        // get player input
        horizontalInput = Input.GetAxisRaw("Horizontal" + playerID);

        isMovingLeft = (horizontalInput < 0);
        isMovingRight = (horizontalInput > 0);


        // player facing
        if(isMovingRight && isFacingLeft) {
            isFacingLeft = false;
            isFacingRight = true;

            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(isMovingLeft && isFacingRight) {
            isFacingLeft = true;
            isFacingRight = false;

            GetComponent<SpriteRenderer>().flipX = true;
        }


        // gameover
        if(transform.position.y <= lowerBound || health == 0) {
            health = 0;
            healthBar.SetHealth(0);

            isDead = true;
        }
    }


    private void FixedUpdate()
    {
        // player moving
        if(isMovingLeft || isMovingRight) {
            if(isCrouch) {
                Vector2 velocity = new Vector2(speed * horizontalInput * Time.fixedDeltaTime * 0.5f, playerRb.velocity.y);
                playerRb.velocity = velocity;
            }
            else {
                Vector2 velocity = new Vector2(speed * horizontalInput * Time.fixedDeltaTime, playerRb.velocity.y);
                playerRb.velocity = velocity;
            }

            playerAnim.SetFloat("Speed", Mathf.Abs(speed*horizontalInput));
        }
        else {
            Vector2 velocity = new Vector2(0, playerRb.velocity.y);
            playerRb.velocity = velocity;

            playerAnim.SetFloat("Speed", 0f);
        }
    }
}
