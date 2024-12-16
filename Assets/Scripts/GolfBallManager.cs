using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GolfBallManager : MonoBehaviour
{
    private Rigidbody2D golfBallRigidbody;
    private IObjectPool<GameObject> pool;


    // Start is called before the first frame update\
    [Header("Golf Ball Settings")]
    [SerializeField][Range(1f, 30f)] private float maxSpeed = 20f;
   
    void Start()
    {
        golfBallRigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetPool(IObjectPool<GameObject> objectPool)
    {
        pool = objectPool;
    }

    public void ReleaseToPool()
    {
        if (pool != null)
        {
            pool.Release(gameObject);
        }
        golfBallRigidbody.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        //Optional: Limit the ball's velocity
        if (golfBallRigidbody.velocity.magnitude > maxSpeed)
        {
            golfBallRigidbody.velocity = golfBallRigidbody.velocity.normalized * maxSpeed;
        }
        // Get the viewport position of the ball
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        
        // Destroy if ball goes off screen
        if (viewportPoint.y < 0 )
        {
            if (pool != null)
            {
                pool.Release(gameObject);
                
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
