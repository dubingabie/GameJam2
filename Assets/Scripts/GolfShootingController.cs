using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class GolfShootingController : MonoBehaviour
{
    [Header("Golf Shooting Settings")]
    [SerializeField] private GameObject golfBall;  // Reference to your golf ball prefab
    [SerializeField] private int defaultPoolCapacity = 10;
    [SerializeField] private int maxPoolSize = 20;
  
    private IObjectPool<GameObject> ballPool;
    //create an object pool for all of the golf balls
    [SerializeField] private float basePower = 5f;
    [SerializeField][Range(1f,200f)] private float maxPower = 20f;
    [SerializeField][Range(1f,100f)] private float powerMultiplier = 100f;

    private float currentPower = 0f;
    private bool isCharging = false;
    private Vector2 shootDirection;
    private Vector2 mousePosition;
    private Vector2 playerPosition;
    [Header("Power Bar Ui")]
    [SerializeField] private GameObject powerBarUI;
    [SerializeField] private Image power;
    [Header("Sound")]
    [SerializeField] private AudioClip chargeSound;
    [SerializeField][Range(0.0f, 3.0f)] private float chargeSoundVolume = 1.0f;
    private AudioSource audioSource;
    
    void Awake()
    {
        //initialize the object pool
        ballPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject ball = Instantiate(golfBall);
                ball.GetComponent<GolfBallManager>().SetPool(ballPool);
                return ball;
            },
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            },
            actionOnRelease: (obj) =>
            {
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                obj.SetActive(false);
            },
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: defaultPoolCapacity,
            maxSize: maxPoolSize
        );
        // make the pool have 10 golf balls
        for (int i = 0; i < defaultPoolCapacity; i++)
        {
            GameObject ball = Instantiate(golfBall);
            ball.GetComponent<GolfBallManager>().SetPool(ballPool);
            ballPool.Release(ball);
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        currentPower = 0f;
        powerBarUI.SetActive(false);   
        power.fillAmount = 0f;
    }

   

    void Update()
    {
        //if (powerBar == null) return;  // Safety check

        // Get mouse position for aiming
      
        // Charging power while holding mouse button
        if (Input.GetMouseButtonDown(0))
        {
            currentPower = 0f;
            isCharging = true;
            powerBarUI.SetActive(true);
            power.fillAmount = 0f;
            audioSource.PlayOneShot(chargeSound,chargeSoundVolume);
            //Debug.Log("Starting charge - Power reset to 0"); // Debug log
        } 
        if(isCharging)
        {
            currentPower += Time.deltaTime * powerMultiplier;
            currentPower = Mathf.Min(currentPower, maxPower);
            power.fillAmount = Mathf.Clamp(basePower + currentPower, 0, maxPower) / maxPower;
            if (currentPower >= maxPower)
            {
                audioSource.Stop();
            }
            //Debug.Log($"Charging - Current Power: {currentPower}, Power Multiplier: {powerMultiplier}"); // Debug power build-up

        }
        if (isCharging && Input.GetMouseButtonUp(0))
        {
            audioSource.Stop();
            isCharging = false;
            currentPower = Mathf.Max(basePower, currentPower);
            currentPower = Mathf.Min(currentPower, maxPower);

            //Debug.Log($"Release - Final Power before shoot: {currentPower}"); // Debug final power
            float shootingPower = currentPower;
            currentPower = 0f;
            ShootBall(shootingPower);
            power.fillAmount = 0f;
            powerBarUI.SetActive(false);
        }
    }
    
    void ShootBall(float shootingPower)
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(temp.x, temp.y);
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        shootDirection = (mousePosition - playerPosition); ;
        shootDirection = shootDirection.normalized;
        
        // Get ball from pool
        GameObject ball = ballPool.Get();
        ball.transform.position = playerPosition;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        
        rb.AddForce(shootDirection * shootingPower, ForceMode2D.Impulse);
        


    }
    
}

