using UnityEngine;

public class WindMill : MonoBehaviour
{
    public float speed = 100f; // degrees per second

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
