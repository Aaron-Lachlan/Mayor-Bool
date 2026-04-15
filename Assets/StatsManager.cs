using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private int Day = 1;
    [SerializeField] private int Money;
    [SerializeField] private int PeopleHappy;
    [SerializeField] private int Pollution;

    public int ChangeDays
    {
        get
        {
            return Day;
        }
        set
        {

            Day += value;

        }
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
}
