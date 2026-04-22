using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public event Action<int> EventNewDay;
    public event Action EventBillPassed;
    public event Action EventBillRejected;
    public event Action EventShowGraph;

    public StatsManager StatsManager;
            
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
}
