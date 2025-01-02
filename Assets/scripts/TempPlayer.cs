using UnityEngine;


public class TempPlayer : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float jumpForce = 10f; // Jump height
    public LayerMask groundLayer; // Layer to check if the player is on the ground
    [SerializeField] GameObject movingMusic;

    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    Animator anim;

    void Start()
    {
        // Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Horizontal movement input
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);
        if(moveInput > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("isWalk", true);
        }
        if (moveInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("isWalk", true);
        }
        if(moveInput == 0)
        {
            anim.SetBool("isWalk", false);
        }

        // Jump when pressing space or up arrow
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (Mathf.Approximately(rb.linearVelocity.y, 0)) // Ensure the player only jumps when vertical velocity is zero
            {
                Jump();
            }
        }
        if (Input.GetButtonDown("Jump")) 
        {
            if (Mathf.Approximately(rb.linearVelocity.y, 0)) // Ensure the player only jumps when vertical velocity is zero
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        // Apply upward force to the player's Rigidbody2D
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Preserve horizontal velocity
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            FindObjectOfType<follow>().zoomInNow();
            GetComponent<AudioSource>().volume = 1;
            movingMusic.GetComponent<AudioSource>().volume = 0.2f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            FindObjectOfType<follow>().zoomOutNow();
            GetComponent<AudioSource>().volume = 0;
            movingMusic.GetComponent<AudioSource>().volume = 1;
        }
    }
}
