using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfShootingController : MonoBehaviour
{
    [Header("Golf Shooting Settings")]
    [SerializeField] private GameObject golfBall;  // Reference to your golf ball prefab
    [SerializeField][Range(1f,30f)] private float maxPower = 20f;
    [SerializeField][Range(1f,100f)] private float powerMultiplier = 100f;
    
    private float currentPower = 0f;
    private bool isCharging = false;
    private Vector2 shootDirection;
    
    void Update()
    {
        // Get mouse position for aiming
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        shootDirection = (mousePosition - playerPosition).normalized;
        
        // Charging power while holding mouse button
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            currentPower = 0f;
        }
        
        if (isCharging)
        {
            currentPower += Time.deltaTime * powerMultiplier;
            currentPower = Mathf.Min(currentPower, maxPower);
        }
        
        // Release to shoot
        if (isCharging && Input.GetMouseButtonUp(0))
        {
            ShootBall();
            isCharging = false;
        }
    }
    
    void ShootBall()
    {
        // Create the golf ball
        GameObject ball = Instantiate(golfBall, transform.position, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        
        // Apply force in the shoot direction
        rb.AddForce(shootDirection * currentPower, ForceMode2D.Impulse);
    }
    
}
