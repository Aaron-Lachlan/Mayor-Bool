using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    Image panel;
    TextMeshProUGUI text;

    public float TimeDayIsDisplayed;
    public float TimeDayIsFaded;

    private void Awake()
    {
        panel = canvas.GetComponentInChildren<Image>();
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine("DayCavansChange");
    }

    public IEnumerator DayCavansChange()
    {


        if (!canvas.activeSelf)
        {
            canvas.SetActive(true);
        }
        if (Time.time > 10)
        {
            float time0To1 = 0f;
            while (time0To1 < TimeDayIsFaded)
            {
                time0To1 += Time.deltaTime;
                float t = Mathf.Clamp01(time0To1 / TimeDayIsFaded);
                //t = EaseInOut(t);
                ChangeLerpTarget(0, 1, t);

                yield return null;
            }

        }


        yield return new WaitForSeconds(TimeDayIsDisplayed);



        float time1To0 = 0f;

        while (time1To0 < TimeDayIsFaded)
        {
            time1To0 += Time.deltaTime;
            float t = Mathf.Clamp01(time1To0 / TimeDayIsFaded);
            //t = EaseInOut(t);
            ChangeLerpTarget(1, 0, t);
  
            yield return null;
        }

        canvas.SetActive(false);
        yield return null;
    }
    private void ChangeLerpTarget(float A,  float B, float t)
    {

        UnityEngine.Color color = panel.color;
        color.a = Mathf.Lerp(1, 0, t);
        panel.color = color;

    }
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}


