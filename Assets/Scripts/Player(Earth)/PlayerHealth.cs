using UnityEngine;
using Microlight.MicroBar;
public class PlayerHealth : MonoBehaviour
{
    //references
    [SerializeField] private MicroBar healthBar;
    //variables
    [SerializeField] private float playerHealth = 1000f;
    [SerializeField] private float maxHealth = 1000f;

    public CurrencyManager currencyManager;
    
    private void Start()
    {
        playerHealth = maxHealth;
        healthBar.Initialize(playerHealth);
    }
    public void TakeDamage(float damage)
    {
        healthBar.UpdateBar(healthBar.CurrentValue - damage);
        playerHealth -= damage;
        if (playerHealth <= 0f)
        {
            // Handle player death (e.g., game over, respawn, etc.)
            Debug.Log("Player is dead!");
        }
    }
    public void Heal(float amount)
    {   
        if (currencyManager.coins >= 200 && playerHealth < maxHealth)
        {
            currencyManager.RemoveCoins(200);
            healthBar.UpdateBar(healthBar.CurrentValue + (healthBar.MaxValue * amount / 100f));
            playerHealth += playerHealth*amount/100f;
            if (playerHealth > maxHealth)
            {
                playerHealth = maxHealth;
            }   
        }
        
    }
}
