using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] GameObject Player;
    int currentCamera = 20;
    int minCamera = 5;
    int maxCamera = 20;
    bool zoomIn = false;
    bool zoomOut = false;
    float zoomSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        

        if(zoomIn)
        {
            GetComponent<Camera>().orthographicSize -= zoomSpeed * Time.deltaTime;
            if(GetComponent<Camera>().orthographicSize <= minCamera)
            {
                zoomIn = false;
            }
        }

        if (zoomOut)
        {
            GetComponent<Camera>().orthographicSize += zoomSpeed * Time.deltaTime;
            if (GetComponent<Camera>().orthographicSize >= maxCamera)
            {
                zoomOut = false;
            }
        }
    }

    public void zoomInNow()
    {
        zoomIn = true;
    }

    public void zoomOutNow()
    {
        zoomOut = true;
    }
}
