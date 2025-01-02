using System.Collections.Generic;
using UnityEngine;


public class Hill_Check : MonoBehaviour
{

    // Collision Check with Player
    // Hill wants 3 stones (always)
    // Subtracts 3 stones and continues hilling around
    private void OnCollisionEnter2D(Collider2D other)
        {
            // Check if the colliding object is the player
            if (other.CompareTag("Player"))
            {
                Inventory inventory = other.GetComponent<Inventory>();
                num_stones = inventory.GetItemNum("Stone");
                if (num_stones == null)
                {
                    Debug.Log("Player does not have a Stone component!");
                    return;
                }

                // Check if the player has at least 3 stones
                if (num_stones >= 3)
                {
                    Debug.Log("Granted");
                    inventory.SetItemNum("Stone", num_stones - 3); // Subtract 3 stones
                }
                else
                {
                    Debug.Log("Denied");
                }
            }
        }                
}