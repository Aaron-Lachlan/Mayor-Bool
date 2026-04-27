using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public event Action<int> EventNewDay;
    public event Action EventBillPassed;
    public event Action EventBillRejected;
    public event Action EventShowGraph;

    public event Action<int,int,int,int> EventSendGraphData;

    public StatsManager StatsManager;

    //Audio definitions
    public AudioSource clickSound;
            
    private void Awake()
    {
        current = this;


        
    }
    private void Start()
    {
        ForceNewDay();
    }
    public void NewDay(int day)
    {
        if (EventNewDay != null)
        {
            EventNewDay(day);
        }

    }
    public void SendGraphData()
    {
        if (EventSendGraphData != null)
        {
            
            EventSendGraphData(StatsManager.Daytest, StatsManager.ChangeMoney, StatsManager.ChangePeopleHappy, StatsManager.ChangePollution);
        }

    }
    public void BillPassed()
    {
        if (EventBillPassed != null)
        {
            EventBillPassed();
        }
    }
    public void BillRejected()
    {
        if (EventBillRejected != null)
        {
            EventBillRejected();
        }
    }
    public void ShowGraph()
    {
        if (EventShowGraph != null)
        {
            EventShowGraph();
        }
    }

    [ContextMenu("Force New Day")]
    public void ForceNewDay()
    {
        GameManager.current.NewDay(StatsManager.Daytest);
    }


    public void AddMoney(int amount)
    {
        if (StatsManager == null)
        {
            Debug.LogError("StatsManager not found.");
            return;
        }

        StatsManager.ChangeMoney += amount;
        Debug.Log("Money changed by: " + amount);
    }

    public void AddHappiness(int amount)
    {
        if (StatsManager == null)
        {
            Debug.LogError("StatsManager not found.");
            return;
        }

        StatsManager.ChangePeopleHappy += amount;
        Debug.Log("Happiness changed by: " + amount);
    }

    public void AddPollution(int amount)
    {
        if (StatsManager == null)
        {
            Debug.LogError("StatsManager not found.");
            return;
        }

        StatsManager.ChangePollution += amount;
        Debug.Log("Pollution changed by: " + amount);
    }

    IEnumerator WaitOne()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
    }

    public void QuitGame()
    {
        print("Wait...");
        StartCoroutine(WaitOne());
        print("Quit!");
        Application.Quit();
    }
}
