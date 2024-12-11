using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shipPrefab;
    public Transform spaceshipsParent;
    public int rowCount = 2;
    public int shipsPerRow = 5;
    void Start()
    {
        SpawnShipFormation();
    }

    void SpawnShipFormation()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int ship = 0; ship < shipsPerRow; ship++)
            {
                Vector3 spawnPosition = new Vector3(
                    ship * 2.5f, 
                    1.5f + row * 3f, 
                    -2f
                );
                Debug.Log(spawnPosition);
                GameObject spawnedShip = Instantiate(shipPrefab, spawnPosition, Quaternion.identity);
                spawnedShip.transform.SetParent(spaceshipsParent);
            }
        }
    }
}