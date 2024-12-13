using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minShootInterval = 1f;
    [SerializeField] private float maxShootInterval = 20f;
    [SerializeField] private float fallSpeed = 1.5f;
    [SerializeField] private Transform shootPoint;

    private void Start()
    {
        StartCoroutine(ShootBallRoutine());
    }

    private System.Collections.IEnumerator ShootBallRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));

            GameObject ball = Instantiate(ballPrefab, 
                shootPoint != null ? shootPoint.position : transform.position, 
                Quaternion.identity);
            //invoke ball move dwon routine in its game object class
            ball.GetComponent<BulletMovementManager>().StartMoving();
            
        }
    }

 
}