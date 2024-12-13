using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] destructionSprites;
    [SerializeField] float frameRate = 0.1f;
    private SpriteRenderer spriteRenderer;
    private bool isDestroying = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            isDestroying = true;

            other.gameObject.SetActive(false);
            //Destroy(gameObject);
            StartCoroutine(PlayDestructionAnimation());

        }
    }
    private IEnumerator PlayDestructionAnimation()
    {
        // Disable collider to prevent multiple hits
        GetComponent<Collider2D>().enabled = false;
        // Play through each destruction frame
        for (int i = 0; i < destructionSprites.Length; i++)
        {
            spriteRenderer.sprite = destructionSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        // Notify game manager and destroy ship
        Destroy(gameObject);
    }
}
