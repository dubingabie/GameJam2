using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementManager : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    // make this method serialized
    
    
    public void StartMoving()
    {
        // Start the coroutine on this object
        StartCoroutine(MoveBallDownRoutine());
    }
    public System.Collections.IEnumerator MoveBallDownRoutine()
    {
        Transform ballTransform = transform;
        Vector3 screenBottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        while (ballTransform.position.y > screenBottomEdge.y)
        {
            ballTransform.position += Vector3.down * (fallSpeed * Time.deltaTime);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
