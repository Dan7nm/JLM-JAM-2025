using UnityEngine;
using TMPro; // Make sure you are using TextMeshPro for better text rendering.

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 20f; // Timer start value in seconds
    public TextMeshProUGUI timerText; // Reference to the UI TextMeshPro component

    void Update()
    {
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
            timeRemaining = 20f;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText == null)
        {
        Debug.LogError("TimerText is not assigned in the Inspector! (Dan Debugger)");
        return;
        }
        
        // Display the timer in MM:SS format
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
       
    }
}
