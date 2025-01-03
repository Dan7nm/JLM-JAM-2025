using UnityEngine;

public class bloodRock : MonoBehaviour
{
    [SerializeField] float lifeDuration = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float currentlife = Random.Range(lifeDuration * 0.7f, lifeDuration * 1.5f);
        Destroy(gameObject, currentlife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
