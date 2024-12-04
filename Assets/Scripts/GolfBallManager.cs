using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D golfBallRigidbody;
    // Start is called before the first frame update\
    [Header("Golf Ball Settings")]
    [SerializeField][Range(1f, 30f)] private float maxSpeed = 10f;
    void Start()
    {
        golfBallRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Limit the ball's velocity
        if (golfBallRigidbody.velocity.magnitude > maxSpeed)
        {
            golfBallRigidbody.velocity = golfBallRigidbody.velocity.normalized * maxSpeed;
        }
    }
}
