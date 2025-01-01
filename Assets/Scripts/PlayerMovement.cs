using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
