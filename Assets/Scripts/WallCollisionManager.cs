using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit the wall");
            //Destroy(other.gameObject);
            // maybe add multiple hits to wall before it gets destroyed
            // make the other object dissapear 
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
}
