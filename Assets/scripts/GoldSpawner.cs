using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockType;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 3;
    [SerializeField] float range = 1.1f;

    [SerializeField] int spawn_num = 1;

    [SerializeField] AudioClip sound;

    private int counter=1;

    float spawnTime;

    // Reference to TimerScript to access remainingTime
    public TimerScript timerScript; // Make sure to assign this in the inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       timerScript = FindFirstObjectByType<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= spawnTime && !rockType.CompareTag("Blood Stone")) 
            {
                Spawn();
            }
        if (Time.timeSinceLevelLoad >= spawnTime && 
            rockType.CompareTag("Blood Stone") && Time.timeSinceLevelLoad >= (counter * timerScript.timerTime))
        {
            counter++;
            Spawn();
            Invoke("booldSound", 1.75f);
            Invoke("booldSound", 2.5f);
            Invoke("booldSound", 3.2f);

        }
    }

    private void Spawn()
    {
        for (int i = 0; i<spawn_num; i++)
        {
        Instantiate(rockType, transform.position + new Vector3(Random.Range(-range, range), 0, 0), Quaternion.identity);
        spawnTime = Time.timeSinceLevelLoad + Random.Range(minDelay, maxDelay);
        }
    }

    private void booldSound()
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, Random.Range(0.2f, 0.6f));
    }
}
