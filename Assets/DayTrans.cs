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
    public GameObject canvas;
    Image panel;
    TextMeshProUGUI text;

    public float TimeDayIsDisplayed;
    public float TimeDayIsFaded;



    private void Awake()
    {
        GameManager.current.EventNewDay += NewDay;

        panel = canvas.GetComponentInChildren<Image>();
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnDestroy()
    {
        GameManager.current.EventNewDay -= NewDay;

    }

    public void NewDay()
    {
        //fix txt Displaying correct day num


        StartCoroutine("DayCavansChange");
    }



    public IEnumerator DayCavansChange()
    {


        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
        }

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

        canvas.SetActive(false);
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


