using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private int Day;
    [SerializeField] private int Money;
    [SerializeField] private int PeopleHappy;
    [SerializeField] private int Pollution;



    void ChangeDays()
    {
        Day++;
    }
    public int ChangeMoney
    {
        get
        {
            return Money;
        }
        set
        {

            Money += value;

        }
    }
    public int ChangePeopleHappy
    {
        get
        {
            return PeopleHappy;
        }
        set
        {

            PeopleHappy += value;

        }
    }
    public int ChangePollution
    {
        get
        {
            return Pollution;
        }
        set
        {

            Pollution += value;

        }
    }

    private void Awake()
    {
        GameManager.current.EventNewDay += ChangeDays;
    }
    private void OnDestroy()
    {
        GameManager.current.EventNewDay -= ChangeDays;

    }
}
