using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

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
}
