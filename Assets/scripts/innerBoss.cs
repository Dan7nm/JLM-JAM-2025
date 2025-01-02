using UnityEngine;

public class innerBoss : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] AudioClip[] soundsBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(soundsBoss[Random.Range(0, soundsBoss.Length)], Camera.main.transform.position);
            path.SetActive(true);
        }
    }
}
