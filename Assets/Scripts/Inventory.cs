using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro support
using static UnityEditor.Progress;
using System;
//using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{

    public int maxBig =0;
    public int maxMed = 0;
    public int maxSmall= 0 ;

    int counter = 0;

    [SerializeField] AudioClip sound;

    private Dictionary<string, int> items = new Dictionary<string, int>
    {
        { "Big Stone", 0 },
        { "Med Stone", 0 },
        { "Small Stone", 0 }
    };

    public TextMeshProUGUI inventoryText; // Reference to the UI text
    public TextMeshProUGUI missionText; // Reference to the UI text

    void Start()
    {
        UpdateInventoryUI(); // Initialize the UI
    }

    public void AddItem(string itemName)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, UnityEngine.Random.Range(0.05f, 0.2f));

        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }
        UpdateInventoryUI();
        // Debug.Log($"{itemName} added. Total: {items[itemName]}");
    }

    // Return the number of a specific item
    public int GetItemNum(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        Debug.LogWarning($"Item '{itemName}' not found in inventory!");
        return 0;
    }

    // Change the number of a specific item
    public void SetItemNum(string itemName, int num)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] = num;
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning($"Item '{itemName}' not found in inventory!");
        }
    }

    private void UpdateInventoryUI()
    {
        // Format the dictionary as a clean string for UI
        string inventoryDisplay = "Inventory:\n";

        
        foreach (var item in items)
        {
            inventoryDisplay += $"{item.Value} \n";
            //inventoryDisplay += $"{item.Key}: {item.Value}\n";

        }
        inventoryText.text = inventoryDisplay.TrimEnd(); // Remove trailing newline
    }

    public void UpdateMission(int big, int med, int small)
    {
        maxBig = big;
        maxMed = med;
        maxSmall = small;
        UpdatemissionUI();
    }

    private void UpdatemissionUI()
    {
        string missionDisplay = $"{maxBig} \n";
        missionDisplay += $"{maxMed} \n";
        missionDisplay += $"{maxSmall} \n";

        missionText.text = missionDisplay.TrimEnd(); // Remove trailing newline

    }
}
