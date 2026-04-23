using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    [System.Serializable]
    public class BuildingSlot
    {
        public string slotName;
        public GameObject buildingObject;
    }

    public List<BuildingSlot> buildingSlots;

    public void ActivateSlot(string slotName)
    {
        foreach (var slot in buildingSlots)
        {
            if (slot.slotName == slotName)
            {
                if (slot.buildingObject != null)
                {
                    slot.buildingObject.SetActive(true);
                    Debug.Log("Activated: " + slotName);
                }
                return;
            }
        }

        Debug.LogWarning("Slot not found: " + slotName);
    }
}
