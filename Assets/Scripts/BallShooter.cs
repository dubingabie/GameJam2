using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minShootInterval = 1f;
    [SerializeField] private float maxShootInterval = 20f;
    [SerializeField] private float fallSpeed = 1.5f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioClip shootSound;
    [SerializeField][Range(0f,3f)] private float shootVolume = 1f;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShootBallRoutine());
    }

    private System.Collections.IEnumerator ShootBallRoutine()
    {
        while (true)
        {
            if (!GameState.isGamePaused)
            {
                yield return new WaitForSeconds(Random.Range(minShootInterval,
                    maxShootInterval));

                GameObject ball = Instantiate(ballPrefab,
                    shootPoint != null
                        ? shootPoint.position
                        : transform.position,
                    Quaternion.identity);
                //invoke ball move dwon routine in its game object class
                audioSource.PlayOneShot(shootSound, shootVolume);
                ball.GetComponent<BulletMovementManager>().StartMoving();
            }
            else{
                yield return null;
            }
        }
    }

 
}