using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    [SerializeField] GameObject movingMusic;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");  // Get input from left/right arrows
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);  // Move the player
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if((other.gameObject.tag == "Stone1") || (other.gameObject.tag == "Stone2") || (other.gameObject.tag == "Stone3"))
        {
            Destroy(other.gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boss")
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
