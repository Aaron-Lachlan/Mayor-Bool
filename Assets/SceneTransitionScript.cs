using UnityEngine;
using System.Collections;


public class SceneTransitionScript : MonoBehaviour
{
    [SerializeField] private RectTransform officePanel;
    [SerializeField] private RectTransform outsideWindowPanel;
    [SerializeField] private RectTransform deskPanel;
    [SerializeField] private float duration = 0.5f;

    public GameObject lookUpButton;
    public GameObject windowButton;
    public GameObject lookDownButton;
    public GameObject cloudObject;
    public GameObject skyObject;

    private Vector2 officeStartPos;
    private Vector2 outsideWindowStartPos;
    private Vector2 deskStartPos;

    private Vector2 officeOffscreenLeft;
    private Vector2 outsideWindowOnscreenPos;

    private Vector2 officeOffscreenUp;
    private Vector2 deskOnscreenPos;

    private void Start()
    {
        officeStartPos = officePanel.anchoredPosition;
        outsideWindowOnscreenPos = officeStartPos;
        deskOnscreenPos = officeStartPos;

        RectTransform parentRect = (RectTransform)officePanel.parent;
        float screenWidth = parentRect.rect.width;
        float screenHeight = parentRect.rect.height;

        officeOffscreenLeft = officeStartPos + new Vector2(-screenWidth, 0);
        outsideWindowStartPos = outsideWindowOnscreenPos + new Vector2(screenWidth, 0);

        officeOffscreenUp = officeStartPos + new Vector2(0, screenHeight);
        deskStartPos = deskOnscreenPos + new Vector2(0, -screenHeight);

        outsideWindowPanel.anchoredPosition = outsideWindowStartPos;
        deskPanel.anchoredPosition = deskStartPos;
    }

    public void EndDay()
    {
        StopAllCoroutines();
        cloudObject.SetActive(false);
        skyObject.SetActive(false);
        lookDownButton.SetActive(false);
        lookUpButton.SetActive(false);
        windowButton.SetActive(false);
        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeOffscreenLeft,
            outsideWindowPanel, outsideWindowPanel.anchoredPosition, outsideWindowOnscreenPos
        ));
    }

    public void StartNewDay()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeStartPos,
            outsideWindowPanel, outsideWindowPanel.anchoredPosition, outsideWindowStartPos
        ));
    }

    public void GoToDesk()
    {
        StopAllCoroutines();
        lookUpButton.SetActive(true);
        lookDownButton.SetActive(false);
        windowButton.SetActive(false);
        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeOffscreenUp,
            deskPanel, deskPanel.anchoredPosition, deskOnscreenPos
        ));


    }

    public void LeaveDesk()
    {
        StopAllCoroutines();
        lookUpButton.SetActive(false);
        lookDownButton.SetActive(true);
        windowButton.SetActive(true);
        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeStartPos,
            deskPanel, deskPanel.anchoredPosition, deskStartPos
        ));

    }

    private IEnumerator SlidePanels(
        RectTransform panelA, Vector2 panelAStart, Vector2 panelAEnd,
        RectTransform panelB, Vector2 panelBStart, Vector2 panelBEnd)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = EaseInOut(t);

            panelA.anchoredPosition = Vector2.Lerp(panelAStart, panelAEnd, t);
            panelB.anchoredPosition = Vector2.Lerp(panelBStart, panelBEnd, t);

            yield return null;
        }

        panelA.anchoredPosition = panelAEnd;
        panelB.anchoredPosition = panelBEnd;
    }

    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
