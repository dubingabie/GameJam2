using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D playerRigidbody;
    [Header("Movment Settings")]
    [SerializeField][Range(1f, 20f)] private float movementSpeed = 5f;
    [SerializeField][Range(1f, 20f)] private float maxSpeed = 10f;
    //private bool isSwinging = false;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && !isSwinging)
        // {
        //     //add swinging mechanic later
        // }
        
        // Move left 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody.AddForce(Vector2.left * movementSpeed);
        }
        
        // Move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody.AddForce(Vector2.right * movementSpeed);
        }
        
        if (playerRigidbody.velocity.magnitude > maxSpeed)
        {
            Vector2 newVelocity = playerRigidbody.velocity.normalized * maxSpeed;
            // Restore the original vertical velocity
            playerRigidbody.velocity = newVelocity;
        }
    }
}
