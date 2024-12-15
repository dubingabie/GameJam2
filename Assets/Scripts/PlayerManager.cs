using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Movement Settings
    [Header("Movement Settings")]
    [SerializeField][Range(1f, 20f)] private float movementSpeed = 5f;
    [SerializeField] private Sprite[] walkingSprites;
    [SerializeField] private float animationSpeed = 0.2f;

    // Shooting Animation Settings
    [Header("Shooting Animation")]
    [SerializeField] private Sprite[] shootingSprites;
    [SerializeField] private float shootingFrameRate = 0.1f;

    // Player Health
    [Header("Player Health")]
    [SerializeField] private float playerMaxHealth = 3;
    [SerializeField] private Image healthImage;
    private float playerCurrentHealth;

    // Damage Animation Settings
    [Header("Damage Animation Settings")]
    [SerializeField] Sprite[] damageSprites;
    [SerializeField] float frameRate = 0.1f;

    // Component References
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    // Movement Variables
    private float moveX = 0f;

    // Animation State Variables
    private float animationTimer;
    private int currentWalkSpriteIndex = 0;
    private bool isDamaged = false;
    private bool isAnimatingShooting = false;
    private bool isMoving = false;

    [SerializeField] private GameOverManager gameOverManager;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        
        playerCurrentHealth = playerMaxHealth;

        // Validation checks
        if (walkingSprites == null || walkingSprites.Length < 2)
        {
            Debug.LogWarning("Please assign at least two walking sprites in the inspector!");
        }
        if (shootingSprites == null || shootingSprites.Length == 0)
        {
            Debug.LogWarning("Please assign shooting sprites in the inspector!");
        }
    }

    void Update()
    {
        // Reset movement flag
        isMoving = false;
        
        // Handle Movement
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
            isMoving = true;
        }
        else
        {
            moveX = 0f;
        }

        // Move the character
        Vector2 movement = new Vector2(moveX * movementSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = movement;

        // Handle character flipping
        FlipTowardsMouse(isMoving);

        // Handle shooting animation
        HandleShootingAnimation();

        // Handle walking animation
        HandleWalkingAnimation();
    }

    void HandleShootingAnimation()
    {
        // Prevent shooting animation if already damaged
        if (isDamaged) return;

        if (Input.GetMouseButtonDown(0) && !isAnimatingShooting)
        {
            StartCoroutine(PlayShootingAnimation());
        }
    }

    private IEnumerator PlayShootingAnimation()
    {
        isAnimatingShooting = true;

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
            yield return new WaitForSeconds(shootingFrameRate);
        }

        // Reset to initial sprite
        if (shootingSprites.Length > 0)
        {
            spriteRenderer.sprite = shootingSprites[0];
        }
        isAnimatingShooting = false;
    }

    void HandleWalkingAnimation()
    {
        // Prefer shooting animation over walking
        if (isAnimatingShooting) return;

        // Prefer damage animation over walking
        if (isDamaged) return;

        if (isMoving)
        {
            AnimateWalking();
        }
        else if (walkingSprites.Length > 0)
        {
            // Reset to first sprite when not moving
            spriteRenderer.sprite = walkingSprites[0];
        }
    }

    void FlipTowardsMouse(bool isMoving)
    {
        // If shooting is active, don't change flip
        if (isAnimatingShooting) return;

        // Get mouse position in world coordinates
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        // Determine if mouse is to the left or right of the player
        if (!isMoving)
        {
            // Flip based on mouse position when not moving
            spriteRenderer.flipX = mousePosition.x < transform.position.x;
        }
        else
        {
            // Flip based on movement direction when moving
            spriteRenderer.flipX = moveX < 0;
        }
    }

    void AnimateWalking()
    {
        // Manage animation timer
        animationTimer += Time.deltaTime;

        // Check if it's time to switch sprites
        if (animationTimer >= animationSpeed)
        {
            // Cycle through walking sprites
            currentWalkSpriteIndex = (currentWalkSpriteIndex + 1) % walkingSprites.Length;
            spriteRenderer.sprite = walkingSprites[currentWalkSpriteIndex];

            // Reset timer
            animationTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("player hit by bullet");
            other.gameObject.SetActive(false);
            playerCurrentHealth--;
            if (!isDamaged)
            {
                isDamaged = true;
                StartCoroutine(PlayDamageAnimation());
            }
            healthImage.fillAmount = playerCurrentHealth / playerMaxHealth;
            
            if (playerCurrentHealth <= 0)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
    
    private IEnumerator PlayDamageAnimation()
    {
        // Play through each destruction frame
        for (int i = 0; i < damageSprites.Length; i++)
        {
            spriteRenderer.sprite = damageSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        isDamaged = false;
    }
}