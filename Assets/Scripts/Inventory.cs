using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro support

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>
    {
        { "Big Stone", 0 },
        { "Med Stone", 0 },
        { "Small Stone", 0 }
    };

    public TextMeshProUGUI inventoryText; // Reference to the UI text

    void Start()
    {
        UpdateInventoryUI(); // Initialize the UI
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }
        UpdateInventoryUI();
        Debug.Log($"{itemName} added. Total: {items[itemName]}");
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
            inventoryDisplay += $"{item.Key}: {item.Value}\n";
        }
        inventoryText.text = inventoryDisplay.TrimEnd(); // Remove trailing newline
    }
}
