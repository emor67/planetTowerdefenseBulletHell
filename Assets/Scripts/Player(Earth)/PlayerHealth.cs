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

    public AudioSource audioSource;
    public AudioClip hurtSound;

    private void Start()
    {
        playerHealth = maxHealth;
        healthBar.Initialize(playerHealth);
    }
    public void TakeDamage(float damage)
    {
        healthBar.UpdateBar(healthBar.CurrentValue - damage);
        playerHealth -= damage;
        audioSource.PlayOneShot(hurtSound);

        if (playerHealth <= 0f)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    public void Heal(float amount)
    {
        if (currencyManager.coins >= 500 && playerHealth < maxHealth)
        {
            currencyManager.RemoveCoins(500);
            healthBar.UpdateBar(healthBar.CurrentValue + (healthBar.MaxValue * amount / 100f));
            playerHealth += playerHealth * amount / 100f;
            if (playerHealth > maxHealth)
            {
                playerHealth = maxHealth;
            }
        }

    }
    
    public void UpgradeMaxHealth(float amount)
    {
        maxHealth += maxHealth * amount / 100f;
        playerHealth = maxHealth;
        healthBar.SetNewMaxHP(maxHealth);
    }
}
