using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterestingMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    public float speed = 2f;
    public float amplitude = 1f;
    public float frequency = 1f;
    private float _randField;

    void Start()
    {
        // Store the initial spawn position
        originalPosition = transform.position;
        _randField = Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        float xOffset = Mathf.PingPong(Time.time * speed, 1f) - 10.1f;
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        Vector3 newPosition = new Vector3(
            originalPosition.x + xOffset,
            originalPosition.y + yOffset * _randField,
            originalPosition.z
        );

        transform.position = newPosition;
    }
}