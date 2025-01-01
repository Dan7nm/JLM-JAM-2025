using System.Collections.Generic;
using UnityEngine;


public class Hill_Check : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            [SerializeField] private string accessDeniedMessage = "You need at least 3 stones to proceed!";
            [SerializeField] private string accessGrantedMessage = "You have enough stones! Get me 3 more!";
            
            private void OnTriggerEnter2D(Collider2D other)
            {
                // Check if the colliding object is the player
                if (other.CompareTag("Player"))
                {
                    PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                    if (player == null)
                    {
                        Debug.LogWarning("Player does not have a PlayerCharacter component!");
                        return;
                    }

                    // Check if the player has at least 3 stones
                    if (player.num_stones >= 3)
                    {
                        Debug.Log(accessGrantedMessage);
                        player.num_stones -= 3; // Subtract 3 stones
                        GrantAccess();
                    }
                    else
                    {
                        Debug.Log(accessDeniedMessage);
                        DenyAccess();
                    }
                }
            }

            private void GrantAccess()
            {
                // Example: Unlock a door, enable a feature, or proceed to the next area
                Debug.Log("Performing access granted actions...");
            }

            private void DenyAccess()
            {
                // Example: Show UI feedback, play a sound, or block the player
                Debug.Log("Performing access denied actions...");
            }
        }
    }
}
