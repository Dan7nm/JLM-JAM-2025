using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using System.Collections; // For TextMeshPro support
using Random = UnityEngine.Random;
using static TaskBoss;

public class gameManager : MonoBehaviour
{
    //required num of rocks in the inventory
    public int bigRocksNeeded = 5;
    public int mediumRocksNeeded = 0;
    public int smallRocksNeeded = 0;
    private Inventory inventory;
    [SerializeField] AudioClip[] happySounds;

    //set texts
    //public TextMeshProUGUI bossText; // Reference to the UI text
    //private List<string> bigRocksTxt;
    //private List<string> medRocksTxt;
    //private List<string> smallRocksTxt;

    //set times
    float textTime;
    //[SerializeField] float textDuration = 3f;
    //public float templeDuration = 2f;

    //set temple objects
    [SerializeField] GameObject templeBig;
    [SerializeField] GameObject templeMed;
    [SerializeField] GameObject templeSmall;

    private bool hasStarted= false;
    private float endMissionTime;
    private float delayBetweenMissions = 2f;
    

    public enum GAME_STATUS { BEGIN_GAME,
        START_FIRST_MISSION, MID_FIRST_MISSION, DONE_FIRST_MISSION,
        START_SECOND_MISSION, MID_SECOND_MISSION, DONE_SECOND_MISSION,
        START_THIRD_MISSION, MID_THIRD_MISSION, DONE_THIRD_MISSION }

    public GAME_STATUS status; //represents the status of the game

    /*  Start is called once before the first execution of Update,
        after the MonoBehaviour is created  */
    void Start()
    {
        status = GAME_STATUS.BEGIN_GAME;
        Debug.Log("Started");
        UpdateNeeded();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == GAME_STATUS.BEGIN_GAME)
        {
            BeginGame();
        }
        // etc... elseif
    }

    private void OnTriggerEnter2D(Collider2D collision) //trigger ('collision') happened
    {
        if (collision.gameObject.tag == "Player") //is the trigger cased by player?
        {
            writeText();
            //check what the players have
            //decide in wich status am I

            if (! hasStarted)
            {
                hasStarted = true;
                status = GAME_STATUS.START_FIRST_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
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
            if (collision.GetComponent<Inventory>().GetItemNum("Big Stone") < bigRocksNeeded &&
                isTimerOk() && (status == GAME_STATUS.START_FIRST_MISSION))
            {
                //here if the player  not have enoght stones
                status = GAME_STATUS.MID_FIRST_MISSION;
            }



            //second mission
            if (status == GAME_STATUS.DONE_FIRST_MISSION &&
                isTimerOk()) 
            {
                status = GAME_STATUS.START_SECOND_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                mediumRocksNeeded = collision.GetComponent<Inventory>().GetItemNum("Med Stone") + 10;
                UpdateNeeded();
                templeBig.SetActive(false);
            }
            if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") >= mediumRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_SECOND_MISSION || status == GAME_STATUS.MID_SECOND_MISSION))
            {
                //here if the player have enoght stones
                status = GAME_STATUS.DONE_SECOND_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                AudioSource.PlayClipAtPoint(happySounds[Random.Range(0, happySounds.Length)], Camera.main.transform.position);
                templeMed.SetActive(true);
            }
            if (collision.GetComponent<Inventory>().GetItemNum("Med Stone") < mediumRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_SECOND_MISSION))
                
            {
                //here if the player  not have enoght stones
                status = GAME_STATUS.MID_SECOND_MISSION;
            }


            //third mission
            if (status == GAME_STATUS.DONE_SECOND_MISSION &&
                isTimerOk())
            {
                status = GAME_STATUS.START_THIRD_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                smallRocksNeeded = collision.GetComponent<Inventory>().GetItemNum("Small Stone") + 10;
                UpdateNeeded();
                templeMed.SetActive(false);
            }

            if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") >= smallRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_THIRD_MISSION || status == GAME_STATUS.MID_THIRD_MISSION))
            {
                //here if the player have enoght stones
                status = GAME_STATUS.DONE_THIRD_MISSION;
                endMissionTime = Time.timeSinceLevelLoad;
                AudioSource.PlayClipAtPoint(happySounds[Random.Range(0, happySounds.Length)], Camera.main.transform.position);
                templeSmall.SetActive(true);
            }
            if (collision.GetComponent<Inventory>().GetItemNum("Small Stone") < smallRocksNeeded &&
                isTimerOk() &&
                (status == GAME_STATUS.START_THIRD_MISSION))
            {
                //here if the player, not have enoght stones
                status = GAME_STATUS.MID_THIRD_MISSION;
            }
        }
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
