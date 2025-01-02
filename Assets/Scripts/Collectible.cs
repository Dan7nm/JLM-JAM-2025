using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemName;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision detected with: " + collision.collider.name);
        if (collision.collider.CompareTag("Player"))
        {
            Inventory inventory = collision.collider.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(gameObject.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player does not have an Inventory component!");
            }
        }
    }
}
