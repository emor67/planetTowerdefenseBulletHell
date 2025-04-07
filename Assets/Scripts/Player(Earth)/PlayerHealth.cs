using UnityEngine;
using Microlight.MicroBar;
public class PlayerHealth : MonoBehaviour
{
    //references
    [SerializeField] private MicroBar healthBar;
    //variables
    [SerializeField] private float playerHealth = 1000f;
    
    private void Start()
    {
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
        healthBar.UpdateBar(healthBar.CurrentValue + amount);
        playerHealth += amount;
        if (playerHealth > 1000f) // Assuming 1000 is the max health
        {
            playerHealth = 1000f;
        }
    }
}
