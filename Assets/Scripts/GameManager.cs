using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] shipPrefabs;
    public int[] typeCount;
    public Transform spaceshipsParent;
    private int rowCount = 1;
    // public int shipsPerRow = 5;
    [SerializeField] GameOverManager gameOverManager;  // Reference to your GameOverManager
    
    private int remainingShips;  // Track remaining ships

    void Start()
    {
        SpawnShipFormation();
        //sum members of type count into remaining ship using delta
        for (int i = 0; i < typeCount.Length; i++)
        {
            remainingShips += typeCount[i];
        }

    }

    void SpawnShipFormation()
    {
        for (int type = 0; type < shipPrefabs.Length; type++)
        {
            for (int row = 0; row < typeCount.Length; row++)
            {
                if (row == type)
                {
                    for (int ship = 0; ship < typeCount[type]; ship++)
                    {
                        Vector3 spawnPosition = new Vector3(
                            9f + ship * 0.5f,
                            -0.25f + row * 0.25f,
                            -2f
                        );
                        Debug.Log(spawnPosition);
                        GameObject spawnedShip = Instantiate(shipPrefabs[type],
                            spawnPosition, Quaternion.identity);
                        spawnedShip.transform.SetParent(spaceshipsParent);

                        // Add a reference to the GameManager in collision manager of each shipp
                        ShipCollisionManager collisionManager =
                            spawnedShip.GetComponent<ShipCollisionManager>();
                        if (collisionManager != null)
                        {
                            collisionManager.gameManager = this;
                        }
                    }
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