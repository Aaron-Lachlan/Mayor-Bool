using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Bill", menuName = "Bills/new bill", order = 1)]
public class BillTemplateSO : ScriptableObject
{
    public string BillName;

    public string ChangeMoneyAmount;
    public string ChangePeopleHappyAmount;
    public string ChangePollutionAmount;
    public bool ShipSlot;
    public bool OceanBuildingSlot;
    public bool AestheticSlot1;
    public bool AestheticSlot2;
    public bool AestheticSlot3;
    public bool AestheticSlot4;
    public bool AestheticSlot5;
    public bool AestheticSlot6;
    public bool BuildingSlot1;
    public bool BuildingSlot2;
    public bool BuildingSlot3;
    public bool BuildingSlot4;
    public bool BuildingSlot5;
    public bool BuildingSlot6;
    public bool BuildingSlot7;
    public bool BuildingSlot8;
    public bool BuildingSlot9;
    public bool BuildingSlot10;
    public List<bool> BuildingSlots;
    public List<string> BuildingSlotNames = new List<string>() { "BuildingSlot1", "BuildingSlot2", "BuildingSlot3", "BuildingSlot4", "BuildingSlot5", "BuildingSlot6", "BuildingSlot7", "BuildingSlot8", "BuildingSlot9", "BuildingSlot10", "AestheticSlot1", "AestheticSlot2", "AestheticSlot3", "AestheticSlot4", "AestheticSlot5", "AestheticSlot6", "OceanBuildingSlot", "ShipSlot" };

    public void Awake()
    {
        BuildingSlots = new List<bool>() { BuildingSlot1, BuildingSlot2, BuildingSlot3, BuildingSlot4, BuildingSlot5, BuildingSlot6, BuildingSlot7, BuildingSlot8, BuildingSlot9, BuildingSlot10, AestheticSlot1, AestheticSlot2, AestheticSlot3, AestheticSlot4, AestheticSlot5, AestheticSlot6, OceanBuildingSlot, ShipSlot };
        
        GameManager.current.EventBillPassed += ApplyBill;
        GameManager.current.EventBillRejected += RejectBill;
        GameManager.current.EventBillPassed += BuildingCheck;
    }
    public void OnDestroy()
    {
        GameManager.current.EventBillPassed -= ApplyBill;
        GameManager.current.EventBillRejected -= RejectBill;
        GameManager.current.EventBillPassed -= BuildingCheck;
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
    public void BuildingCheck()
    {
        for (int i = 0; i < BuildingSlots.Count; i++)
        {
            if (BuildingSlots[i])
            {
                bool slot = BuildingSlots[i];
                if (slot == true)
                {
                    GameObject building = GameObject.Find(BuildingSlotNames[i]);
                    if (building != null)
                    {
                        building.SetActive(true);
                    }
                }
            }
        }
    }
}
