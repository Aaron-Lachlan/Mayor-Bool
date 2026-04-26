using UnityEngine;

public class ButtonEndDay : MonoBehaviour
{
    public GameObject GraphCanvas;

    public void OnGraphEndDayPress()
    {
        GraphCanvas.SetActive(true);
        
    }
    public void onEndDayPress()
    {
        GraphCanvas.SetActive(false);
        GameManager.current.ForceNewDay();
    }

}
