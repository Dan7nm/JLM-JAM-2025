using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro support

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
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

    // Return the  number of a specific item.
    public int GetItemNum(string itemName){
        return items[itemName];
    }

    // Change the number of a specific item
    public void SetItemNum(string itemName, int num)
    {
        items[itemName] = num;
    }

    private void UpdateInventoryUI()
    {
        // Update the UI text to display all items in the inventory
        inventoryText.text = "Inventory:\n" + string.Join("\n", items);
    }
}
