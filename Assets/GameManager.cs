using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public event Action EventNewDay;

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

    [ContextMenu("Force New Day")]
    public void ForceNewDay()
    {
        GameManager.current.NewDay();
    }
}
