using UnityEngine;

public class gold : MonoBehaviour
{
    [SerializeField] int hitCounter;
    [SerializeField] int maxHit = 6;
    [SerializeField] Sprite[] sprites;
    [SerializeField] AudioClip[] hitSounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hitCounter >= maxHit)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCounter++;
        if(hitCounter == 1)
        {
            AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0, hitSounds.Length)], Camera.main.transform.position, 0.2f);
        }
        
    }
}
