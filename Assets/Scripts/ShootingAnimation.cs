using UnityEngine;

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
        // Detect mouse click
        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            isAnimating = true;
            currentFrame = 0;
            StartCoroutine(PlayAnimation());
        }
    }

    private System.Collections.IEnumerator PlayAnimation()
    {
        while (currentFrame < shootingSprites.Length)
        {
            spriteRenderer.sprite = shootingSprites[currentFrame];
            currentFrame++;
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