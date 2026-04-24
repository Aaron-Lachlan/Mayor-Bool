using UnityEngine;
using System.Collections;
public class BillSlideOnDesk : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private string targetObjectName;

    [Header("Movement")]
    [SerializeField] private float duration = 0.5f;

    //public AudioClip stampSound;
    public AudioSource audioSource;

    private RectTransform rectTransform;
    private RectTransform target;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        FindTarget();
        MoveToTarget();
    }

    private void FindTarget()
    {
        GameObject targetObject = GameObject.Find(targetObjectName);

        if (targetObject == null)
        {
            Debug.LogError("Target object not found: " + targetObjectName);
            return;
        }

        target = targetObject.GetComponent<RectTransform>();

        if (target == null)
        {
            Debug.LogError("Target does not have a RectTransform: " + targetObjectName);
        }
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = target.anchoredPosition;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, time / duration);
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }
}
