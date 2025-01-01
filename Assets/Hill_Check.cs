using System.Collections.Generic;
using UnityEngine;


public class Hill_Check : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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
                    Debug.Log("Granted");
                    player.num_stones -= 3; // Subtract 3 stones
                }
                else
                {
                    Debug.Log("Denied");
                }
            }
        }                
}