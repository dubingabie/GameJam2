using UnityEngine;

public class InterestingMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    public float speed = 2f;
    public float amplitude = 1f;
    public float frequency = 1f;

    void Start()
    {
        // Store the initial spawn position
        originalPosition = transform.position;
    }

    void Update()
    {
        float xOffset = Mathf.PingPong(Time.time * speed, 20f) - 10f;
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        Vector3 newPosition = new Vector3(
            originalPosition.x + xOffset,
            originalPosition.y + yOffset,
            originalPosition.z
        );

        transform.position = newPosition;
    }
}