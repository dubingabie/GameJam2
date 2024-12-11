using UnityEngine;
using System.Collections;

public class ShootingAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] shootingSprites; // Automatically populate from sliced sprites
    [SerializeField] float frameRate = 0.1f;   // Time between each frame
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private bool isAnimating = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to Shoot0
        if (shootingSprites.Length > 0)
        {
            spriteRenderer.sprite = shootingSprites[0];
        }
    }

    void Update()
    {
        // Detect mouse press and release
        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            isAnimating = true;
            currentFrame = 0;
            StartCoroutine(PlayAnimation());
        }
    }

    private System.Collections.IEnumerator PlayAnimation()
    {
        // Move to first frame
        spriteRenderer.sprite = shootingSprites[0];

        // Wait until mouse is pressed
        yield return new WaitUntil(() => Input.GetMouseButton(0));

        // Stay on second frame while mouse is held
        if (shootingSprites.Length > 1)
        {
            spriteRenderer.sprite = shootingSprites[1];
        }

        // Wait until mouse is released
        yield return new WaitUntil(() => !Input.GetMouseButton(0));

        // Continue animation from third frame onwards
        for (int i = 2; i < shootingSprites.Length; i++)
        {
            spriteRenderer.sprite = shootingSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        // Reset to Shoot0 after animation
        if (shootingSprites.Length > 0)
        {
            spriteRenderer.sprite = shootingSprites[0];
        }
        isAnimating = false;
    }
}