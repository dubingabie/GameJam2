using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;
    public Transform spaceshipsParent;
    public int rowCount = 2;
    public int shipsPerRow = 5;
    [SerializeField] GameOverManager gameOverManager;  // Reference to your GameOverManager
    
    private int remainingShips;  // Track remaining ships

    void Start()
    {
        SpawnShipFormation();
        remainingShips = rowCount * shipsPerRow;
    }

    void SpawnShipFormation()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int ship = 0; ship < shipsPerRow; ship++)
            {
                Vector3 spawnPosition = new Vector3(
                    9f+ship * 0.5f, 
                     row * 0.25f, 
                    -2f
                );
                Debug.Log(spawnPosition);
                GameObject spawnedShip = Instantiate(shipPrefab, spawnPosition, Quaternion.identity);
                spawnedShip.transform.SetParent(spaceshipsParent);
                
                // Add a reference to the GameManager in collision manager of each shipp
                ShipCollisionManager collisionManager = spawnedShip.GetComponent<ShipCollisionManager>();
                if (collisionManager != null)
                {
                    collisionManager.gameManager = this;
                }
                
            }
        }
    }
    public void OnShipDestroyed()
    {
        remainingShips--;
        Debug.Log($"Ships remaining: {remainingShips}");
        
        if (remainingShips <= 0)
        {
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }
}