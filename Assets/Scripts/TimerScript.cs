using UnityEngine;
using TMPro; // Make sure you are using TextMeshPro for better text rendering.

public class TimerScript : MonoBehaviour
{
    [SerializeField] public float timerTime = 20f; // Timer start value in seconds
    [SerializeField] public TextMeshProUGUI timerText; // Reference to the UI TextMeshPro component

    public float intervalForSat = 10.0f;
    public float lastIvterChange = 0 ;

    void Update()
    {
        UpdateTimerUI();

        // every 10 sec, change the value of satisfaction in -1
        if (Time.timeSinceLevelLoad - lastIvterChange >= intervalForSat)
        {
            lastIvterChange = Time.timeSinceLevelLoad;
            Debug.Log("ten sec passesed");
            FindObjectOfType<SatisfactionScript>().changeSatAfterTenSec();
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
        float remainingTime = timerTime - Time.timeSinceLevelLoad % timerTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
       
    }
}
