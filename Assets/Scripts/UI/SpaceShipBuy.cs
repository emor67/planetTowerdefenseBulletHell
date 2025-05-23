using UnityEngine;

public class SpaceShipBuy : MonoBehaviour
{
    public GameObject spaceshipPrefab;
    public Transform spawnPoint;
    public CurrencyManager currencyManager;
    public int spaceshipCost = 200; // Cost of the spaceship

    public bool isAlive = false;

    public void SpawnSpaceship()
    {
        // Check if the player has enough currency to buy the spaceship
        if (currencyManager.coins >= spaceshipCost)
        {
            if (spaceshipPrefab != null && spawnPoint != null && !isAlive)
            {
                currencyManager.RemoveCoins(spaceshipCost);
                isAlive = true; // Mark as purchased
                Instantiate(spaceshipPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else return;
        }
        
    }
}
