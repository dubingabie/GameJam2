using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minShootInterval = 1f;
    [SerializeField] private float maxShootInterval = 3f;
    [SerializeField] private float fallSpeed = 5f;
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

            StartCoroutine(MoveBallDownRoutine(ball));
        }
    }

    private System.Collections.IEnumerator MoveBallDownRoutine(GameObject ball)
    {
        Transform ballTransform = ball.transform;
        Vector3 screenBottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        while (ballTransform.position.y > screenBottomEdge.y)
        {
            ballTransform.position += Vector3.down * (fallSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(ball);
    }
}