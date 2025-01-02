using UnityEngine;

public class Hill_Check : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        if (collision.collider.CompareTag("Player"))
        {
            Inventory inventory = collision.collider.GetComponent<Inventory>();
            if (inventory == null)
            {
                Debug.LogWarning("Player does not have an Inventory component!");
                return;
            }

            // Get the number of stones in the inventory
            int num_stones = inventory.GetItemNum("Stone");

            // Check if the player has at least 3 stones
            if (num_stones >= 3)
            {
                Debug.Log("Access Granted");
                inventory.SetItemNum("Stone", num_stones - 3); // Subtract 3 stones
            }
            else
            {
                Debug.Log("Access Denied: Not enough stones!");
            }
        }
    }
}
