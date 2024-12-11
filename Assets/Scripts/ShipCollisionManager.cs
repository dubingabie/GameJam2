using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionManager : MonoBehaviour
{   
     public GameManager gameManager;

     [SerializeField] Sprite[] destructionSprites; // Your 3 destruction frame sprites
     private ParticleSystemRenderer[] jetEngineRenderer;
     [SerializeField] float frameRate = 0.1f;   // Time between each frame
     private SpriteRenderer spriteRenderer;
     private bool isDestroying = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        jetEngineRenderer = GetComponentsInChildren<ParticleSystemRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GolfBall") && !isDestroying)
        {
            Debug.Log("golf ball hit the ship");
            //Destroy(other.gameObject);\
            // trigger on ship destroy method of the game manager
            isDestroying = true;
            //gameManager.OnShipDestroyed();
            Destroy(other.gameObject);
            
            StartCoroutine(PlayDestructionAnimation());
            
        }
    }
    
    private IEnumerator PlayDestructionAnimation()
    {
        // Disable collider to prevent multiple hits
        GetComponent<Collider2D>().enabled = false;
        foreach (ParticleSystemRenderer psr in jetEngineRenderer)
        {
            psr.enabled = false;
        }
        // Play through each destruction frame
        for (int i = 0; i < destructionSprites.Length; i++)
        {
            spriteRenderer.sprite = destructionSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        // Notify game manager and destroy ship
        gameManager.OnShipDestroyed();
        Destroy(gameObject);
    }
}
