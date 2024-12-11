using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionManager : MonoBehaviour
{   
     public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            Debug.Log("golf ball hit the ship");
            //Destroy(other.gameObject);
            // trigger on ship destroy method of the game manager
            gameManager.OnShipDestroyed();
            Destroy(gameObject);
        }
    }
}
