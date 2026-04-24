using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class BillManager : MonoBehaviour
{
    [SerializeField] private GameObject billPrefab;
    [SerializeField] private RectTransform spawnPoint;
    [SerializeField] private RectTransform billParent;
    [SerializeField] private BillTemplateSO[] billTemplates;
    [SerializeField] private int billAmount = 5;

    [SerializeField] private RectTransform acceptTarget;
    [SerializeField] private RectTransform rejectTarget;
    [SerializeField] private float moveDuration = 0.5f;

    [SerializeField] private SceneTransitionScript sceneTransitionScript;

    public GameObject endDayButton;

    private GameObject currentBillObject;
    private BillScript currentBill;

    public AudioSource audioSource;
    public AudioClip newBillSlideIn;
    public AudioClip acceptedBillSlide;
    public AudioClip rejectedBillSlide;

   

    public void SpawnNextBill()
    {
        if (billAmount <= 0)
        {
            Debug.Log("No bills left.");
            
            return;

            

        }

        if (billPrefab == null)
        {
            Debug.LogError("Bill prefab is missing.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is missing.");
            return;
        }

        if (billParent == null)
        {
            Debug.LogError("Bill parent is missing.");
            return;
        }

        if (billTemplates == null || billTemplates.Length == 0)
        {
            Debug.LogError("No bill templates assigned.");
            return;
        }

        BillTemplateSO chosenTemplate = billTemplates[Random.Range(0, billTemplates.Length)];

        currentBillObject = Instantiate(billPrefab, billParent);

        RectTransform billRect = currentBillObject.GetComponent<RectTransform>();
        if (billRect != null)
        {
            billRect.SetParent(billParent, false);

            billRect.position = spawnPoint.position;

            
            billRect.localScale = new Vector3(6f, 6f, 6f);

            billRect.localRotation = Quaternion.identity;
        }

        currentBill = currentBillObject.GetComponent<BillScript>();
        if (currentBill != null)
        {
            currentBill.Setup(chosenTemplate, this);
        }
        else
        {
            Debug.LogError("Spawned bill prefab is missing BillScript.");
        }
    }

    public void ResolveBill(bool accepted)
    {
        if (currentBillObject == null) return;

        StartCoroutine(MoveAndResolve(accepted));
    }
    private IEnumerator MoveAndResolve(bool accepted)
    {
        RectTransform billRect = currentBillObject.GetComponent<RectTransform>();

        if (billRect == null)
        {
            Debug.LogError("Bill has no RectTransform.");
            yield break;
        }

        RectTransform target = accepted ? acceptTarget : rejectTarget;

        if (target == null)
        {
            Debug.LogError("Target is not assigned.");
            yield break;
        }

        Vector3 startPos = billRect.position;
        Vector3 endPos = target.position;

        float time = 0f;

        audioSource.PlayOneShot(acceptedBillSlide);
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            billRect.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        billRect.position = endPos;

        // small delay for feel (optional)
        yield return new WaitForSeconds(0.1f);

        Destroy(currentBillObject);

        currentBillObject = null;
        currentBill = null;

        billAmount--;

        Debug.Log("Bill resolved. Accepted: " + accepted + " | Bills left: " + billAmount);

        if (billAmount > 0)
        {
            SpawnNextBill();
        }
        else
        {
            sceneTransitionScript.BackToOffice();
            endDayButton.SetActive(true);
        }
    }
}
