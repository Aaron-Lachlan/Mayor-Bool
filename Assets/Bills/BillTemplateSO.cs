using UnityEngine;

[CreateAssetMenu(fileName = "Bill", menuName = "Bills/new bill", order = 1)]
public class BillTemplateSO : ScriptableObject
{
    public string BillName;

    public string ChangeMoneyAmount;
    public string ChangePeopleHappyAmount;
    public string ChangePollutionAmount;

}
