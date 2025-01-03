using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections; // For TextMeshPro support
using Random = UnityEngine.Random;
using static TaskBoss;

public class gameManager : MonoBehaviour
{
    //required num of rocks in the inventory
    public int bigRocksNeeded = 0;
    public int mediumRocksNeeded = 0;
    public int smallRocksNeeded = 0;
    private Inventory inventory;
    [SerializeField] AudioClip[] happySounds;

    //set texts
    public TextMeshProUGUI bossText; // Reference to the UI text

    //set times
    float textTime;
    [SerializeField] float textDuration = 3f;

    //set temple objects
    [SerializeField] GameObject templeBig;
    [SerializeField] GameObject templeMed;
    [SerializeField] GameObject templeSmall;

    private bool hasStarted = false;
    private float endMissionTime;
    private float delayBetweenMissions = 2f;


    public enum GAME_STATUS
    {
        BEGIN_GAME,
        START_FIRST_MISSION, MID_FIRST_MISSION, DONE_FIRST_MISSION,
        START_SECOND_MISSION, MID_SECOND_MISSION, DONE_SECOND_MISSION,
        START_THIRD_MISSION, MID_THIRD_MISSION, DONE_THIRD_MISSION
    }

    public GAME_STATUS status; //represents the status of the game

    /*  Start is called once before the first execution of Update,
        after the MonoBehaviour is created  */
    void Start()
    {
        status = GAME_STATUS.BEGIN_GAME;
        Debug.Log("Started");

        writeText();
        UpdateNeeded();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - textTime >= textDuration)
        {
            bossText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //trigger ('collision') happened
    {
        if (collision.gameObject.tag == "Player") //is the trigger cased by player?
        {
            //check what the players have
            //decide in wich status am I

            if (!hasStarted)
            {
                hasStarted = true;
                status = GAME_STATUS.START_FIRST_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                bigRocksNeeded = 5;
                UpdateNeeded();
                //here we get the first mission :)
            }

            //first mission
            if (collision.GetComponent<Inventory>().GetItemNum("Big Stone") >= bigRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_FIRST_MISSION || status == GAME_STATUS.MID_FIRST_MISSION))
            {
                //here if the player have enoght stones
                status = GAME_STATUS.DONE_FIRST_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                templeBig.SetActive(true);
                AudioSource.PlayClipAtPoint(happySounds[Random.Range(0, happySounds.Length)], Camera.main.transform.position);
            }
            else if (collision.GetComponent<Inventory>().GetItemNum("Big Stone") < bigRocksNeeded &&
                isTimerOk() && (status == GAME_STATUS.START_FIRST_MISSION))
            {
                //here if the player  not have enoght stones
                status = GAME_STATUS.MID_FIRST_MISSION;
            }



            //second mission
            else if (status == GAME_STATUS.DONE_FIRST_MISSION &&
                isTimerOk())
            {
                status = GAME_STATUS.START_SECOND_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                mediumRocksNeeded = collision.GetComponent<Inventory>().GetItemNum("Med Stone") + 10;
                UpdateNeeded();
                templeBig.SetActive(false);
            }
            else if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") >= mediumRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_SECOND_MISSION || status == GAME_STATUS.MID_SECOND_MISSION))
            {
                //here if the player have enoght stones
                status = GAME_STATUS.DONE_SECOND_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                AudioSource.PlayClipAtPoint(happySounds[Random.Range(0, happySounds.Length)], Camera.main.transform.position);
                templeMed.SetActive(true);
            }
            else if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") < mediumRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_SECOND_MISSION))

            {
                //here if the player  not have enoght stones
                status = GAME_STATUS.MID_SECOND_MISSION;
            }


            //third mission
            else if (status == GAME_STATUS.DONE_SECOND_MISSION &&
                isTimerOk())
            {
                status = GAME_STATUS.START_THIRD_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                smallRocksNeeded = collision.GetComponent<Inventory>().GetItemNum("Small Stone") + 10;
                UpdateNeeded();
                templeMed.SetActive(false);
            }

            else if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") >= smallRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_THIRD_MISSION || status == GAME_STATUS.MID_THIRD_MISSION))
            {
                //here if the player have enoght stones
                status = GAME_STATUS.DONE_THIRD_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                AudioSource.PlayClipAtPoint(happySounds[Random.Range(0, happySounds.Length)], Camera.main.transform.position);
                templeSmall.SetActive(true);
                Invoke("win", 9);
            }
            else if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") < smallRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_THIRD_MISSION))
            {
                //here if the player, not have enoght stones
                status = GAME_STATUS.MID_THIRD_MISSION;
            }

            writeText();
        }
    }

    private void win()
    {
        FindObjectOfType<LevelLoader>().LoadNext();
    }

    private void BeginGame()
    {
        //bossText.text = $"Collect {bigRocksNeeded} big rocks for me. Surely, this is within even your limited abilities.";
        //status = GAME_STATUS.WAITING_BIG;
        //subTaskStatus = SUBTASK_STATUS.STARTED;
        //collision.GetComponent<Inventory>().SetItemNum("Big Stone", 0);
    }
    private void writeText()
    {
        //write the relevant text depend on the text
        bossText.gameObject.SetActive(true);
        textTime = Time.time;

        switch (status)
        {
            case GAME_STATUS.BEGIN_GAME:
                bossText.text = Texts.welcomeMsg;
                break;
            case GAME_STATUS.START_FIRST_MISSION:
                bossText.text = Texts.getTaskBigMsg(bigRocksNeeded);
                break;
            case GAME_STATUS.START_SECOND_MISSION:
                bossText.text = Texts.getTaskMedMsg(mediumRocksNeeded);
                break;
            case GAME_STATUS.START_THIRD_MISSION:
                bossText.text = Texts.getTaskSmallMsg(smallRocksNeeded);
                break;
            case GAME_STATUS.MID_FIRST_MISSION:
                bossText.text = Texts.getMidBigMsg();
                break;
            case GAME_STATUS.MID_SECOND_MISSION:
                bossText.text = Texts.getMidMedMsg();
                break;
            case GAME_STATUS.MID_THIRD_MISSION:
                bossText.text = Texts.getMidSmallMsg();
                break;
            case GAME_STATUS.DONE_FIRST_MISSION:
                bossText.text = Texts.getDoneBigMsg();
                break;
            case GAME_STATUS.DONE_SECOND_MISSION:
                bossText.text = Texts.getDoneMedMsg();
                break;
            case GAME_STATUS.DONE_THIRD_MISSION:
                bossText.text = Texts.getDoneSmallMsg();
                break;
        }
    }

    private void UpdateNeeded()
    {
        FindObjectOfType<Inventory>().UpdateMission(bigRocksNeeded, mediumRocksNeeded, smallRocksNeeded);
    }
    private bool isTimerOk()
    {
        return Time.timeSinceLevelLoad - endMissionTime >= delayBetweenMissions;
    }
}

public static class Texts
{
    public static List<string> bigRocksTxt = new List<string>
    {
        "Are we perhaps misunderstanding the concept of 'big rocks'? Do try again.",
        "More, mortal, more! Return only with enough!",
        "Bring what I demand, or prepare to join the rubble!",
        "Do you fancy playing with pebbles while I wait for big rocks?",
        "Fail again, and your fate will be etched in stone!"
    };

    public static List<string> medRocksTxt = new List<string>
    {
        "Medium rocks? More like medium effort. Try again.",
        "Are these rocks or skipping stones? Bring proper ones!",
        "I asked for medium rocks, not your mediocre attempts.",
        "This feels like a test of my divine patience. Bring them now!",
        "I swear, the next failure will see you crushed under your excuses."
    };

    public static List<string> smallRocksTxt = new List<string>
    {
        "Small rocks, small effort. Don't make me repeat myself!",
        "These look more like grains than rocks. Fix it!",
        "Do you plan to insult me with these crumbs of stone?",
        "Collect proper small rocks, or I'll make you as insignificant!",
        "One more misstep, and your name will vanish like dust."
    };

    public static string welcomeMsg = "Welcome to my domain, mortal. Do try not to trip over your own incompetence as we begin.";

    public static string getTaskBigMsg(int bigRocksNeeded)
    {
        return $"Collect {bigRocksNeeded} big rocks for me. Surely, this is within even your limited abilities.";
    }

    public static string getTaskMedMsg(int medRocksNeeded)
    {
        return $"Now, bring me {medRocksNeeded} medium rocks. Chop-chop!";
    }

    public static string getTaskSmallMsg(int smallRocksNeeded)
    {
        return $"Fetch {smallRocksNeeded} small rocks. " +
                        "They're like the garnish on this culinary masterpiece of incompetence."; ;
    }

    public static string getMidBigMsg()
    {
        return bigRocksTxt[Random.Range(0, bigRocksTxt.Count)];
    }

    public static string getMidMedMsg()
    {
        return medRocksTxt[Random.Range(0, medRocksTxt.Count)];
    }

    public static string getMidSmallMsg()
    {
        return smallRocksTxt[Random.Range(0, smallRocksTxt.Count)];
    }

    public static string getDoneBigMsg()
    {
        return "Ah, yes, the big rocks. Not bad, though I expected at least some flair.";
    }

    public static string getDoneMedMsg()
    {
        return "Medium rocks, eh? Such ambition!";
    }

    public static string getDoneSmallMsg()
    {
        return "Truly, your persistence is as surprising as " +
                        "it is unnecessary.";
    }
}