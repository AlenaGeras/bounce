using System.Security.Cryptography;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed = 10000f;
    private float jumpForce = 150f;
    private float maxVelocity = 10f;
    private float gravity = 6f;
    private float waterSlowdownFactor = 0.1f;

    private bool isInWater = false;
    private float originalMoveSpeed;
    private float horizontalInput;

    [SerializeField] private AudioClip wallAudio;
    [SerializeField] private AudioClip waterAudio;
    [SerializeField] private AudioClip ringAudio;
    [SerializeField] private AudioClip bonusAudio;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }


    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }


    private void HandleMovement()
    {

        float currentMoveSpeed = isInWater ? originalMoveSpeed * waterSlowdownFactor : moveSpeed;


        Vector2 horizontalMovement = new Vector2(horizontalInput * currentMoveSpeed, 0f);

        rb.AddForce(horizontalMovement * Time.fixedDeltaTime, ForceMode2D.Force);
        if (isInWater)
        {
            rb.AddForce(Vector2.up * 0.5f, ForceMode2D.Force);
        }

        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -maxVelocity, maxVelocity),
            rb.linearVelocity.y
        );
    }


    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            gameObject.GetComponent<AudioSource>().clip = wallAudio;
            if (Mathf.Abs(rb.linearVelocityY) > 5f)
                gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.tag == "Water")
        {
            gameObject.GetComponent<AudioSource>().clip = waterAudio;
            if (Mathf.Abs(rb.linearVelocityY) > 3f)
                gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.GetComponent<PassRing>())
        {
            gameObject.GetComponent<AudioSource>().clip = ringAudio;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.GetComponent<HealthPickup>())
        {
            gameObject.GetComponent<AudioSource>().clip = bonusAudio;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            rb.gravityScale = -gravity / 10f;


            moveSpeed = originalMoveSpeed * waterSlowdownFactor;

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            isInWater = false;
            rb.gravityScale = gravity;
            moveSpeed = originalMoveSpeed;
        }
    }
}
