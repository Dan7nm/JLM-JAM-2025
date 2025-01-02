using UnityEngine;
using TMPro; // Make sure you are using TextMeshPro for better text rendering.

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 60f; // Timer start value in seconds
    public TextMeshProUGUI timerText; // Reference to the UI TextMeshPro component

    public float intervalForSat = 10.0f;
    public float lastIvterChange = 0 ;
    
    void Update()
    {
        //every 10 sec, change the value of satisfaction in -1
        if (Time.timeSinceLevelLoad - lastIvterChange >= intervalForSat)
        {
            lastIvterChange = Time.timeSinceLevelLoad;
            Debug.Log("ten sec passesed");
            FindObjectOfType<SatisfactionScript>().changeSatAfterTenSec();
        }


        // Decrease timer over time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            // Timer reaches zero
            timeRemaining = 0;
            // Add your logic for when the timer finishes (e.g., game over or next level)
            Debug.Log("Time's up!");
            timeRemaining = 60f;
        }
    }

    void UpdateTimerUI()
    {
        // Display the timer in MM:SS format
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
}
