using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ShootingAnimation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Sprite[] shootingSprites; // Automatically populate from sliced sprites
    [SerializeField] float frameRate = 0.1f;   // Time between each frame
    private SpriteRenderer spriteRenderer;
    private bool isAnimating = false;
    [Header("Audio")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField][Range(0.0f, 3.0f)] private float volume = 1.0f;
    private AudioSource audioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        // Set the initial sprite to Shoot0
        if (shootingSprites.Length > 0)
        {
            spriteRenderer.sprite = shootingSprites[0];
        }
        // Get the AudioSource component if not assigned
  
    }

    void Update()
    {
        // Detect mouse press and release
        if (Input.GetMouseButtonDown(0) && !isAnimating)
        {
            isAnimating = true;
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
        audioSource.PlayOneShot(shootSound,volume);
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