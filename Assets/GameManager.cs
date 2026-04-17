using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public event Action EventNewDay;
    public event Action EventBillPassed;
    public event Action EventBillRejected;

    private void Awake()
    {
        current = this;


        
    }
    private void Start()
    {
        ForceNewDay();
    }
    public void NewDay()
    {
        if (EventNewDay != null)
        {
            EventNewDay();
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

    [ContextMenu("Force New Day")]
    public void ForceNewDay()
    {
        GameManager.current.NewDay();
    }
}
