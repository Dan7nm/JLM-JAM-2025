using UnityEngine;

public class testcode : MonoBehaviour
{

    public int numOfWoods;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        numOfWoods++;
    }


}
