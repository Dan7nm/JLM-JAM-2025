using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockType;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 3;
    [SerializeField] float range = 1.1f;
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
        Debug.Log($"Timer: {timerScript.timeRemaining}");
        if (Time.timeSinceLevelLoad >= spawnTime && 
            (rockType.CompareTag("Blood Stone") ? timerScript.timeRemaining <= 0 : true))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(rockType, transform.position + new Vector3(Random.Range(-range, range), 0, 0), Quaternion.identity);
        spawnTime = Time.timeSinceLevelLoad + Random.Range(minDelay, maxDelay);
    }
}
