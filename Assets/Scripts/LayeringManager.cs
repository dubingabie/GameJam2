using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeringManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject grass; // Changed to GameObject
    // add same functionality but for walls
    
    [SerializeField][Range(0f, 10f)] private float grassOutlineHeight = 0f;
    //private bool isOverGrass = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Error checking
        if (grass == null)
        {
            Debug.LogError("Grass reference not set! Please assign in inspector.");
        }
    }
    
    void Update()
    {
        // if (grass != null)
        // {
            // If ball is above grass, render it in front
            if (transform.position.y > (grass.transform.position.y+grass.transform.localScale.y/2)+grassOutlineHeight)
            {
                //isOverGrass = true;
                spriteRenderer.sortingLayerName = "Background";
            }
            // If ball is below grass, render it behind
            // else
            // {
            //     if (isOverGrass)
            //     
            //         spriteRenderer.sortingLayerName = "Background";
            //     }
            // }
        // }
    }

}
