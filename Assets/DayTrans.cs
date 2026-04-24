using System;
using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DayTrans : MonoBehaviour
{
    public GameObject loadingCanvas;
    public GameObject menuCanvas;
    Image panel;
    TextMeshProUGUI text;

    public float TimeDayIsDisplayed;
    public float TimeDayIsFaded;

    public int DisplayDay;

    private void Start()
    {
        //GameManager.current.EventNewDay += NewDay;

        panel = loadingCanvas.GetComponentInChildren<Image>();
        text = loadingCanvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        GameManager.current.EventNewDay -= NewDay;

    }

    IEnumerator WaitOne()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
    }
    
    public void LoadDayOne()
    {
        print("Wait...");
        StartCoroutine(WaitOne());
        print("Play!");
        //GameManager.current.EventNewDay += NewDay;
        DisplayDay += 1;
        //order of events with the event system cause this to run before +1 is added to the day on StatsManager
        //the +1 is to compensate for that

        StartCoroutine("DayCavansChange");
    }

    public void NewDay(int day)
    {
        

        DisplayDay = day + 1;
        //order of events with the event system cause this to run before +1 is added to the day on StatsManager
        //the +1 is to compensate for that

        StartCoroutine("DayCavansChange");
    }



    public IEnumerator DayCavansChange()
    {


        if (!loadingCanvas.activeSelf)
        {
            loadingCanvas.SetActive(true);
            menuCanvas.SetActive(false);
            BoolSceneManager.current.LoadNewDay();
        }

        text.text = "Day: " + DisplayDay;

        //fade in (also w/ check for 1st boot)
        if (Time.time > TimeDayIsDisplayed + TimeDayIsFaded)
        {
            
            float time0To1 = 0f;
            while (time0To1 < TimeDayIsFaded)
            {
                time0To1 += Time.deltaTime;
                float t = Mathf.Clamp01(time0To1 / TimeDayIsFaded);
                //t = EaseInOut(t);
                ChangeLerpTargetPanel(0, 1, t);
                ChangeLerpTargetText(0, 1, t);
                yield return null;
            }

        }


        yield return new WaitForSeconds(TimeDayIsDisplayed);





        //fade out
        float time1To0 = 0f;
        while (time1To0 < TimeDayIsFaded)
        {
            time1To0 += Time.deltaTime;
            float t = Mathf.Clamp01(time1To0 / TimeDayIsFaded);
            //t = EaseInOut(t);
            ChangeLerpTargetPanel(1, 0, t);
            ChangeLerpTargetText(1, 0, t);

            yield return null;
        }

        loadingCanvas.SetActive(false);
        yield return null;
    }
    private void ChangeLerpTargetPanel(float A, float B, float t)
    {

        UnityEngine.Color color = panel.color;
        color.a = Mathf.Lerp(A, B, t);
        panel.color = color;

    }
    private void ChangeLerpTargetText(float A, float B, float t)
    {
        text.alpha = Mathf.Lerp(A, B, t);
    }
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}


