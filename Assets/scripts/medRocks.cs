using UnityEngine;

public class medRocks : MonoBehaviour
{
    bool freeFall = true;
    [SerializeField] float jump = 1;
    [SerializeField] float force = 2;
    float hitTime;
    [SerializeField] Sprite[] sprites;

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
        if(Time.timeSinceLevelLoad - hitTime >= 1.5f)
        {
            freeFall = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(freeFall)
        {
            hitTime = Time.timeSinceLevelLoad;
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(force, jump);
            freeFall = false;
            force = force * (-1);
        }
    }
}
