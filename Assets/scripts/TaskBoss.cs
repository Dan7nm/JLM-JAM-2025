using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections; // For TextMeshPro support
using Random = UnityEngine.Random;

public class TaskBoss : MonoBehaviour
{
    //required num of rocks in the inventory
    public int bigRocksNeeded = 5;
    public int mediumRocksNeeded = 20;
    public int smallRocksNeeded = 20;

    private Inventory inventory;
    //bool hasStarted = false;

    public TextMeshProUGUI bossText; // Reference to the UI text

    private List<string> bigRocksTxt;
    private List<string> medRocksTxt;
    private List<string> smallRocksTxt;


    float textTime;
    [SerializeField] float textDuration = 3f;
    public float templeDuration = 2f;
    [SerializeField] GameObject templeBig;
    [SerializeField] GameObject templeMed;
    [SerializeField] GameObject templeSmall;
    //int cnt_collisions = 0;

    public enum TASK_STATUS { BEGIN, WAITING_BIG, WAITING_MED, WAITING_SMALL, DONE }
    public enum SUBTASK_STATUS { STARTED, MID, DONE }

    TASK_STATUS status;
    SUBTASK_STATUS subTaskStatus;

    void Start()
    {
        bigRocksTxt = new List<string>
    {
        "Are we perhaps misunderstanding the concept of 'big rocks'? Do try again.",
        "More, mortal, more! Return only with enough!",
        "Bring what I demand, or prepare to join the rubble!",
        "Do you fancy playing with pebbles while I wait for big rocks?",
        "Fail again, and your fate will be etched in stone!"
    };

        medRocksTxt = new List<string>
    {
        "Medium rocks? More like medium effort. Try again.",
        "Are these rocks or skipping stones? Bring proper ones!",
        "I asked for medium rocks, not your mediocre attempts.",
        "This feels like a test of my divine patience. Bring them now!",
        "I swear, the next failure will see you crushed under your excuses."
    };

        smallRocksTxt = new List<string>
    {
        "Small rocks, small effort. Don't make me repeat myself!",
        "These look more like grains than rocks. Fix it!",
        "Do you plan to insult me with these crumbs of stone?",
        "Collect proper small rocks, or I'll make you as insignificant!",
        "One more misstep, and your name will vanish like dust."
    };

        bossText.text = "Welcome to my domain, mortal. Do try not to trip over your own incompetence as we begin.";

        //bossText.gameObject.SetActive(false);

        status = TASK_STATUS.BEGIN;
        subTaskStatus = SUBTASK_STATUS.STARTED;
        Debug.Log("Started");
    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad - textTime >= textDuration)
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

            if (status == TASK_STATUS.BEGIN)
            {
                bossText.text = $"Collect {bigRocksNeeded} big rocks for me. Surely, this is within even your limited abilities.";
                status = TASK_STATUS.WAITING_BIG;
                subTaskStatus = SUBTASK_STATUS.STARTED;
                //cnt_collisions = 0;
                collision.GetComponent<Inventory>().SetItemNum("Big Stone", 0);
            }

            else if (status == TASK_STATUS.WAITING_BIG)
            {
                //cnt_collisions++;
                if (collision.GetComponent<Inventory>().GetItemNum("Big Stone") >= bigRocksNeeded)
                {
                    //back after temple
                    if (subTaskStatus == SUBTASK_STATUS.DONE)
                    {
                        //cnt_collisions=0;
                        //templeBig.gameObject.SetActive(false);
                        StartCoroutine(DisableWithDelay(templeBig, templeDuration, () =>
                        {
                            collision.GetComponent<Inventory>().SetItemNum("Med Stone", 0);

                            bossText.text = $"Now, bring me {mediumRocksNeeded} medium rocks. Chop-chop!";
                            status = TASK_STATUS.WAITING_MED;
                            subTaskStatus = SUBTASK_STATUS.STARTED;
                        }));
                    }
                    else
                    {
                        bossText.text = "Ah, yes, the big rocks. Not bad, though I expected at least some flair.";
                        subTaskStatus = SUBTASK_STATUS.DONE;
                        //TEMPLE
                        templeBig.gameObject.SetActive(true);

                    }
                }

                //not enough big rocks
                else
                {
                    subTaskStatus = SUBTASK_STATUS.MID;
                    bossText.text = bigRocksTxt[Random.Range(0, bigRocksTxt.Count)];
                }
            }

            if (status == TASK_STATUS.WAITING_MED)
            {
                if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") >= mediumRocksNeeded)
                {
                    //back after temple
                    if (subTaskStatus == SUBTASK_STATUS.DONE)
                    {
                        StartCoroutine(DisableWithDelay(templeMed, templeDuration));

                        bossText.text = $"Fetch {smallRocksNeeded} small rocks. " +
                        "They're like the garnish on this culinary masterpiece of incompetence.";
                        status = TASK_STATUS.WAITING_SMALL;
                        subTaskStatus = SUBTASK_STATUS.STARTED;
                        // cnt_collisions = 0;
                        templeMed.gameObject.SetActive(false);
                        collision.GetComponent<Inventory>().SetItemNum("Small Stone", 0);
                    }
                    else
                    {
                        bossText.text = "Medium rocks, eh? Such ambition!";
                        subTaskStatus = SUBTASK_STATUS.DONE;
                        //TEMPLE
                        templeMed.gameObject.SetActive(true);
                    }

                }
                else
                {
                    subTaskStatus = SUBTASK_STATUS.MID;
                    bossText.text = medRocksTxt[Random.Range(0, medRocksTxt.Count)];
                }
            }

            if (status == TASK_STATUS.WAITING_SMALL)
            {
                if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") >= smallRocksNeeded)
                {
                    //back after temple
                    if (subTaskStatus == SUBTASK_STATUS.DONE)
                    {
                        bossText.text = "Truly, your persistence is as surprising as " +
                        "it is unnecessary.";

                        templeMed.gameObject.SetActive(false);
                    }
                    else
                    {
                        bossText.text = "Finally, my masterpiece is complete!";
                        subTaskStatus = SUBTASK_STATUS.DONE;
                        //TEMPLE
                        templeSmall.gameObject.SetActive(true);
                    }
                }
                else
                {
                    subTaskStatus = SUBTASK_STATUS.MID;
                    bossText.text = smallRocksTxt[Random.Range(0, smallRocksTxt.Count)];
                }
            }

        }
    }

    public SUBTASK_STATUS taskSubStatus()
    {
        return subTaskStatus;
    }

    // Coroutine to disable GameObject after a delay
    private IEnumerator DisableWithDelay(GameObject target, float delay, Action callback = null)
    {
        target.SetActive(true); // Enable the GameObject
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        target.SetActive(false); // Disable the GameObject
        callback?.Invoke();
    }
}
