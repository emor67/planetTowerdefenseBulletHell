using UnityEngine;
using TMPro;

public class CardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private string cardTextString;
    [SerializeField] private TextMeshProUGUI cardCostText;
    [SerializeField] private int cardCost;

    public CurrencyManager currencyManager;
    public PlayerHealth playerHealth;
    public Turret turret1;
    public Turret turret2;
    public Turret turret3;
    public Turret turret4;
    public SpaceShip spaceShip;
    public Bullet bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardCostText.text = cardCost.ToString();
        cardText.text = cardTextString;

        
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    

    //Buttons
    public void UpgradeMaxHealth()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            playerHealth.UpgradeMaxHealth(100f); // Increase max health by 100 points
        }
        else return;
    }

    public void UpgradeSniperTurretDamage()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            bullet.bulletDamage += bullet.bulletDamage * 0.1f; // Increase damage by 10%
            
        }
        else return;
    }

    public void UpgradeAutomaticTurretDamage()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            bullet.bulletDamage += bullet.bulletDamage * 0.1f; // Increase damage by 10%
            
        }
        else return;
    }

    public void UpgradeAutomaticTurretFireRate()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            turret1.bulletPerSecond += turret1.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret2.bulletPerSecond += turret2.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret3.bulletPerSecond += turret3.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret4.bulletPerSecond += turret4.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            
        }
        else return;
    }

    public void UpgradeSniperTurretFireRate()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            turret1.bulletPerSecond += turret1.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret2.bulletPerSecond += turret2.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret3.bulletPerSecond += turret3.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            turret4.bulletPerSecond += turret4.bulletPerSecond * 0.05f; // Increase fire rate by 5%
            
        }
        else return;
    }

    public void UpgradeAutomaticTurretRange()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            turret1.targetingRange += turret1.targetingRange * 0.1f; // Increase range by 10%
            turret2.targetingRange += turret2.targetingRange * 0.1f; // Increase range by 10%
            turret3.targetingRange += turret3.targetingRange * 0.1f; // Increase range by 10%
            turret4.targetingRange += turret4.targetingRange * 0.1f; // Increase range by 10%
            
        }
        else return;
    }

    public void UpgradeSniperTurretRange()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            turret1.targetingRange += turret1.targetingRange * 0.1f; // Increase range by 10%
            turret2.targetingRange += turret2.targetingRange * 0.1f; // Increase range by 10%
            turret3.targetingRange += turret3.targetingRange * 0.1f; // Increase range by 10%
            turret4.targetingRange += turret4.targetingRange * 0.1f; // Increase range by 10%
            
        }
        else return;
    }

    public void UpgradeSpaceShipDamage()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            bullet.bulletDamage += bullet.bulletDamage * 0.1f; // Increase damage by 10%
            
        }
        else return;
    }

    public void UpgradeSpaceShipProjectileSpeed()
    {
        if (currencyManager.coins >= cardCost)
        {
            currencyManager.RemoveCoins(cardCost);
            spaceShip.projectileSpeed += spaceShip.projectileSpeed * 0.1f; // Increase projectile speed by 10%
            
        }
        else return;
    }
}
