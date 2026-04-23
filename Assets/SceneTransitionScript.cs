using UnityEngine;
using System.Collections;


public class SceneTransitionScript : MonoBehaviour
{
    [SerializeField] private RectTransform officePanel;
    [SerializeField] private RectTransform outsideWindowPanel;
    [SerializeField] private RectTransform deskPanel;
    [SerializeField] private float duration = 0.5f;

    
    public GameObject windowButton;
    
    public GameObject cloudObject;
    public GameObject officeButton;

    public GameObject doorOpen, doorClosed;

    public GameObject windowPanel;

    [SerializeField] private BillManager billManager;



    private Vector2 officeOnscreenPos;
    private Vector2 outsideWindowOnscreenPos;
    private Vector2 deskOnscreenPos;

    private Vector2 officeOffscreenLeft;
    private Vector2 outsideWindowOffscreenRight;
    private Vector2 officeOffscreenUp;
    private Vector2 deskOffscreenDown;

    private void Start()
    {
        
        RectTransform parentRect = (RectTransform)outsideWindowPanel.parent;
        float screenWidth = parentRect.rect.width;
        float screenHeight = parentRect.rect.height;

        // Window starts onscreen first
        outsideWindowOnscreenPos = outsideWindowPanel.anchoredPosition;

        // Office and desk both slide into the same visible position
        officeOnscreenPos = outsideWindowOnscreenPos;
        deskOnscreenPos = outsideWindowOnscreenPos;

        officeOffscreenLeft = officeOnscreenPos + new Vector2(-screenWidth, 0);
        outsideWindowOffscreenRight = outsideWindowOnscreenPos + new Vector2(screenWidth, 0);
        officeOffscreenUp = officeOnscreenPos + new Vector2(0, screenHeight);
        deskOffscreenDown = deskOnscreenPos + new Vector2(0, -screenHeight);

        // Force starting layout
        outsideWindowPanel.anchoredPosition = outsideWindowOnscreenPos;
        officePanel.anchoredPosition = officeOffscreenLeft;
        deskPanel.anchoredPosition = deskOffscreenDown;

        // Starting buttons for window scene
        
        windowButton.SetActive(false);

        if (cloudObject != null)
            cloudObject.SetActive(false);
    }

    // Window -> Office
    public void StartNewDay()
    {
        StopAllCoroutines();

        officeButton.SetActive(false);
        
       

        

        StartCoroutine(SlidePanels(
            deskPanel, deskPanel.anchoredPosition, deskOffscreenDown,
            officePanel, officePanel.anchoredPosition, officeOnscreenPos
        ));

        StartCoroutine(WaitToActivateDoors());

    }
    public IEnumerator WaitToActivateDoors()
    {
        yield return new WaitForSeconds(3f);

        doorClosed.SetActive(false);
        doorOpen.SetActive(true);

        

        yield return new WaitForSeconds(3f);

        GoToDesk();
        
        StartCoroutine(WaitToActivateBills());
        

    }
    public IEnumerator WaitToActivateBills()
    {
        yield return new WaitForSeconds(3f);

        billManager.SpawnNextBill();

        doorClosed.SetActive(true);
        doorOpen.SetActive(false);

        
    }


    // Office -> Window
    public void EndDay()
    {
        
        StopAllCoroutines();

        
        windowButton.SetActive(false);
        officeButton.SetActive(true);

        if (cloudObject != null)
            cloudObject.SetActive(false);

        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeOffscreenLeft,
            outsideWindowPanel, outsideWindowPanel.anchoredPosition, outsideWindowOnscreenPos
        ));
    }


    // Office -> Desk
    public void GoToDesk()
    {
        StopAllCoroutines();

        
        windowButton.SetActive(false);

        StartCoroutine(SlidePanels(
            officePanel, officePanel.anchoredPosition, officeOffscreenUp,
            deskPanel, deskPanel.anchoredPosition, deskOnscreenPos
        ));
    }

    // Desk -> Office
    public void BackToOffice()
    {
        StopAllCoroutines();

        officeButton.SetActive(false);
        
        

        StartCoroutine(SlidePanels(
            deskPanel, deskPanel.anchoredPosition, deskOffscreenDown,
            officePanel, officePanel.anchoredPosition, officeOnscreenPos
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
