using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] GameObject gold;
    [SerializeField] float minDelay = 1f;
    [SerializeField] float maxDelay = 3;
    [SerializeField] float range = 1.1f;
    float spawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad >= spawnTime)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Instantiate(gold, transform.position + new Vector3(Random.Range(-range, range), 0, 0), Quaternion.identity);
        spawnTime = Time.timeSinceLevelLoad + Random.Range(minDelay, maxDelay);
    }


}
