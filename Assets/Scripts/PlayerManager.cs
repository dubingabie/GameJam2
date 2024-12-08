using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D playerRigidbody;
    [Header("Movment Settings")]
    [SerializeField][Range(1f, 20f)] private float movementSpeed = 5f;
    // [SerializeField][Range(1f, 20f)] private float maxSpeed = 10f;
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
        
        // // Move left 
        // if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
        // {
        //     playerRigidbody.AddForce(Vector2.left * movementSpeed);
        // }
        //
        // // Move right
        // if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        // {
        //     playerRigidbody.AddForce(Vector2.right * movementSpeed);
        // }
        //
        // if (playerRigidbody.velocity.magnitude > maxSpeed)
        // {
        //     Vector2 newVelocity = playerRigidbody.velocity.normalized * maxSpeed;
        //     // Restore the original vertical velocity
        //     playerRigidbody.velocity = newVelocity;
        // }
        
        // Get horizontal input (A/D or Left/Right arrows)
        float moveX = 0f;
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }

        // Move the character
        Vector2 movement = new Vector2(moveX * movementSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("player hit by bullet");
        }
    }
}
