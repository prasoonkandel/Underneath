using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{
    public bool TEMPCHECK;

    public GameObject FloorMap;
    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    public Transform groundCheck,invertCheck;
    public LayerMask groundLayer, InvertedFloor,NonInvertedArea;
    public bool isGrounded;
    public float groundCheckRadius, invertCheckRadius;
    public bool canInvert , touchingNonInvertArea, inverted;
    float gravity;
    float YSpeed;
    float horizontalMultiplier =1;
    float mult;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;

    }

    void Update()
    {

        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -40f, 40f);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        Collider2D NoninvertHit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, NonInvertedArea);

        touchingNonInvertArea = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, NonInvertedArea);



        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            FindFirstObjectByType<AudioManager>().PlaySFX(FindFirstObjectByType<AudioManager>().Jump);
            rb.linearVelocityY = jumpSpeed * Mathf.Sign(rb.gravityScale);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = 0f;
        }



        if (inverted)
        {
            FloorMap.SetActive(true);
            transform.localScale = new Vector2(horizontalMultiplier, -1);
            if (rb.linearVelocityY < 0)
            {
                rb.gravityScale = -2.5f * mult;
            }
            else
            {
                rb.gravityScale = -4f * mult;
            }
                GameObject.FindWithTag("InvertFloor").GetComponent<Collider2D>().enabled = false;
            if (touchingNonInvertArea && rb.linearVelocityY > 5f)
            {
                transform.position = NoninvertHit.ClosestPoint(transform.position);
                inverted = false;

            }
        }
        else
        {
            FloorMap.SetActive(false);
            if (rb.linearVelocityY > 0)
            {
                rb.gravityScale = 2.5f;
            }
            else
            {
                rb.gravityScale = 4f;
            }
            transform.localScale = new Vector2(horizontalMultiplier, 1);
            GameObject.FindWithTag("InvertFloor").GetComponent<Collider2D>().enabled = true;
            rb.gravityScale = gravity;
        }

        if (canInvert && Input.GetKey(KeyCode.S) && rb.linearVelocityY < -10f)
        {
            inverted = true;
            YSpeed = rb.linearVelocityY;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = canInvert ? Color.green : Color.red;
        Gizmos.DrawWireSphere(invertCheck.position, invertCheckRadius);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

    }
    private void FixedUpdate()
    {
        LayerMask mask = 1 << 6;

        canInvert = Physics2D.OverlapCircle(invertCheck.position, invertCheckRadius, InvertedFloor);
        bool canNotflipGravity = Physics2D.OverlapCircle(invertCheck.position, invertCheckRadius,mask );


        if (canNotflipGravity)
        {
            mult = -1;
        }
        else
        {
            mult = 1;
        }
            Time.fixedDeltaTime = 0.01f;
        if (!inverted)
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontalMultiplier = 1;
                rb.linearVelocityX = moveSpeed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontalMultiplier = -1;
                rb.linearVelocityX = -moveSpeed;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontalMultiplier = 1;
                rb.linearVelocityX = moveSpeed/1.5f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontalMultiplier = -1;
                rb.linearVelocityX = -moveSpeed/1.5f;
            }

        }

    }
}
