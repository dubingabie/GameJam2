using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D playerRigidbody;
    [Header("Movment Settings")]
    [SerializeField][Range(1f, 20f)] private float movementSpeed = 5f;
    // [SerializeField][Range(1f, 20f)] private float maxSpeed = 10f;
    //private bool isSwinging = false;
    
    [Header("Player Health")]
    [SerializeField] private float playerMaxHealth = 3;
    //[SerializeField] private Image[] healthImages;// - option for several hearts , more fitting in my opinion
    [SerializeField] private Image healthImage;
    private float playerCurrentHealth;
    
    [Header("Damage Animation Settings")]
    [SerializeField] Sprite[] damageSprites;
    [SerializeField] float frameRate = 0.1f;
    private SpriteRenderer spriteRenderer;
    private bool isDamaged = false;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCurrentHealth = playerMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // //initialize health images with intervals from the top left corner
        // for (int i = 0; i < healthImages.Length; i++)
        // {
        //     healthImages[i].rectTransform.anchoredPosition = new Vector2(20 + i * 40, -20);
        // }
        // // //show all health images
        // // for (int i = 0; i < playerMaxHealth; i++)
        // // {
        // //     healthImages[i].enabled = true;
        // // }
    }
    [SerializeField] private GameOverManager gameOverManager;


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
            other.gameObject.SetActive(false);
            playerCurrentHealth--;
            if (!isDamaged)
            {
                isDamaged = true;
                StartCoroutine(PlayDamageAnimation());
            }
            healthImage.fillAmount = playerCurrentHealth / playerMaxHealth;
            //healthImages[(int)playerCurrentHealth].enabled = false;
            if (playerCurrentHealth <= 0)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
    
    private IEnumerator PlayDamageAnimation()
    {
        // // Disable collider to prevent multiple hits
        // GetComponent<Collider2D>().enabled = false;
        // Play through each destruction frame
        for (int i = 0; i < damageSprites.Length; i++)
        {
            spriteRenderer.sprite = damageSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        isDamaged = false;

    }
}
