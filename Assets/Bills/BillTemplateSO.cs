using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Bill", menuName = "Bills/new bill", order = 1)]
public class BillTemplateSO : ScriptableObject
{
    public string BillName;

    public string ChangeMoneyAmount;
    public string ChangePeopleHappyAmount;
    public string ChangePollutionAmount;

    public void Awake()
    {
        GameManager.current.EventBillRejected += ApplyBill;
        GameManager.current.EventBillRejected += RejectBill;
    }
    public void OnDestroy()
    {
        GameManager.current.EventBillPassed -= ApplyBill;
        GameManager.current.EventBillRejected -= RejectBill;
    }
    public void ApplyBill()
    {
        StatsManager statsManager = GameManager.FindObjectOfType<StatsManager>();
        statsManager.ChangeMoney += int.Parse(ChangeMoneyAmount);
        statsManager.ChangePeopleHappy += int.Parse(ChangePeopleHappyAmount);
        statsManager.ChangePollution += int.Parse(ChangePollutionAmount);
        //return to pool
    }
    public void RejectBill()
    {
        //return to pool
    }
}
