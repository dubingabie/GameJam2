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
//
// public class InterestingMovement : MonoBehaviour
// {
//     private Vector3 originalPosition;
//     [SerializeField] public float speed = 2f;
//     [SerializeField] public float amplitude = 1f;
//     [SerializeField] public float frequency = 1f;
//     //[SerializeField] public float movementRange = 3f;  // Reduced from 10f to keep ships more visible
//     private float _randField;
//     private float _direction = 1f;  // Direction multiplier
//     private float _nextFlipTime;    // When to flip next
//     [SerializeField]public float flipInterval = 3f; // Time between flips
//    
//     void Start()
//     {
//         originalPosition = transform.position;
//         _randField = Random.Range(0.5f, 1.5f);
//         _nextFlipTime = Time.time + flipInterval;
//     }
//
//     void Update()
//     {
//         // Check if it's time to flip
//         if (Time.time >= _nextFlipTime)
//         {
//             _direction *= -1f;  // Flip the direction
//             _nextFlipTime = Time.time + flipInterval;  // Set next flip time
//         }
//
//         float xOffset = Mathf.PingPong(Time.time * speed, 1) - 10.1f;
//         float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
//
//         Vector3 newPosition = new Vector3(
//             originalPosition.x + xOffset,//* _direction,  // Apply direction to x movement
//             originalPosition.y + yOffset * _randField,
//             originalPosition.z
//         );
//
//         transform.position = newPosition;
//     }
// }