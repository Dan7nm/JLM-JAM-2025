using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro support

public class TaskBoss : MonoBehaviour
{
    //required num of rocks in the inventory
    public int bigRocksNeeded = 4;
    public int mediumRocksNeeded = 4;
    public int smallRocksNeeded = 4;

    private Inventory inventory;
    bool hasStarted = false;

    public TextMeshProUGUI bossText; // Reference to the UI text

    float textTime;
    [SerializeField] float textDuration = 6;


    enum TASK_STATUS { WAITING_BIG, WAITING_MED, WAITING_SMALL, DONE }
    TASK_STATUS status;

    void Start()
    {
        bossText.gameObject.SetActive(false);

        status = TASK_STATUS.WAITING_BIG;
        Debug.Log("Started");
        
    }
    private void Update()
    {
        if(Time.timeSinceLevelLoad - textTime >= textDuration)
        {
            bossText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            textTime = Time.timeSinceLevelLoad;

            bossText.gameObject.SetActive(true);

            if (!hasStarted)
            {
                bossText.text = "Welcome to my domain, mortal. Do try not to trip over your own incompetence as we begin." +
            "Collect 4 big rocks for me. Surely, this is within even your limited abilities.";
                hasStarted = true;
            }
            
            else if (status == TASK_STATUS.WAITING_BIG)
            {
                if (collision.GetComponent<Inventory>().GetItemNum("Big Stone") >= bigRocksNeeded)
                {
                    bossText.text = "Ah, yes, the big rocks. Not bad, though I expected at least some flair. " +
                        "Now, bring me 4 medium rocks. Chop-chop!";
                    status = TASK_STATUS.WAITING_MED;
                }
                else bossText.text = "Are we perhaps misunderstanding the concept of 'big rocks'? Do try again.";
            }

            if (status == TASK_STATUS.WAITING_MED)
            {
                if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") >= mediumRocksNeeded)
                {
                    bossText.text = "Medium rocks, eh? Such ambition! But we�re not done yet. Fetch 4 small rocks. " +
                        "They�re like the garnish on this culinary masterpiece of incompetence.";
                    status = TASK_STATUS.WAITING_SMALL;
                }
                else bossText.text = "Medium rocks, you say? These are medium only in disappointment. Retry, if you please.";
            }

            if (status == TASK_STATUS.WAITING_SMALL)
            {
                if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") >= smallRocksNeeded)
                {
                    bossText.text = "Finally, my masterpiece is complete! Truly, your persistence is as surprising as " +
                        "it is unnecessary.";
                    status = TASK_STATUS.DONE;
                }
                else bossText.text = "Small rocks should be simple, like your approach. Yet, somehow, still wrong.";
            }

        }
    }

    


}
