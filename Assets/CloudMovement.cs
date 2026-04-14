using UnityEngine;
using UnityEngine.UI;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;

    [SerializeField] private float axisX, axisY;

    private void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(axisX, axisY) * Time.deltaTime, rawImage.uvRect.size);
    }
}
