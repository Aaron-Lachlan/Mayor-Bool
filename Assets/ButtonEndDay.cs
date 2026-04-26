using TMPro;
using UnityEngine;

public class ButtonEndDay : MonoBehaviour
{
    public GameObject GraphCanvas;
    public TextMeshProUGUI statsText;

    public void OnGraphEndDayPress()
    {
        GraphCanvas.SetActive(true);

        GameManager.current.SendGraphData();
    }
    public void onEndDayPress()
    {
        GraphCanvas.SetActive(false);
        GameManager.current.ForceNewDay();
    }
    void UpdateStats(int day, int money, int happiness, int pollution)
    {
        
        statsText.text =
            "End of day stats\n" +
            "Day: " + day + "\n" +
            "Money: " + money + "\n" +
            "City happiness: " + happiness + "\n" +
            "Pollution: " + pollution;
    }
    private void Start()
    {
        GameManager.current.EventSendGraphData += UpdateStats;
    }
    private void OnDestroy()
    {
        GameManager.current.EventSendGraphData -= UpdateStats;
    }
}
