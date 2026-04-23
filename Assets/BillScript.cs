using UnityEngine;

public class BillScript : MonoBehaviour
{
    private BillTemplateSO template;
    private BillManager manager;
    private bool hasBeenResolved = false;

    public void Setup(BillTemplateSO newTemplate, BillManager newManager)
    {
        template = newTemplate;
        manager = newManager;

        gameObject.name = template.BillName;

        Debug.Log("Spawned bill: " + template.BillName);
        Debug.Log("Money: " + template.ChangeMoneyAmount +
                  ", People Happy: " + template.ChangePeopleHappyAmount +
                  ", Pollution: " + template.ChangePollutionAmount);
    }

    public void AcceptBill()
    {
        if (hasBeenResolved) return;
        hasBeenResolved = true;

        ApplyEffects();
        template.BuildingCheck();

        if (manager != null)
        {
            manager.ResolveBill(true);
        }
    }

    public void RejectBill()
    {
        if (hasBeenResolved) return;
        hasBeenResolved = true;

        // Reject does not apply effects in this version.
        // If you want reject to also do something, add it here.

        if (manager != null)
        {
            manager.ResolveBill(false);
        }
    }

    private void ApplyEffects()
    {
        Debug.Log("Applying bill effects for: " + template.BillName);

        Debug.Log("Money change: " + template.ChangeMoneyAmount);
        Debug.Log("People happiness change: " + template.ChangePeopleHappyAmount);
        Debug.Log("Pollution change: " + template.ChangePollutionAmount);

        template.ApplyBill();
        
    }
}
