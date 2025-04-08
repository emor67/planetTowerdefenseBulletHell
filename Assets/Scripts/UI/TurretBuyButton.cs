using UnityEngine;
using TMPro;

public class TurretBuyButton : MonoBehaviour
{
    //references
    [SerializeField] private GameObject automaticTurretPrefab;
    [SerializeField] private GameObject sniperTurretPrefab;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private TextMeshProUGUI automaticTurretCostText;
    [SerializeField] private TextMeshProUGUI sniperTurretCostText;


    //variables
    [SerializeField] private int automaticTurretCost;
    [SerializeField] private int sniperTurretCost;

    void Start()
    {
        automaticTurretCostText.text = automaticTurretCost.ToString();
        sniperTurretCostText.text = sniperTurretCost.ToString();
    }
    public void BuyAutomaticTurret(){
        if(currencyManager.coins >= automaticTurretCost){
            
            currencyManager.RemoveCoins(automaticTurretCost);
            
            automaticTurretPrefab.SetActive(true);
            sniperTurretPrefab.SetActive(false);
        }
        
    }
    public void BuySniperTurret(){
        if(currencyManager.coins >= sniperTurretCost){
            
            currencyManager.RemoveCoins(sniperTurretCost);
            
            sniperTurretPrefab.SetActive(true);
            automaticTurretPrefab.SetActive(false);
        }
    }
}
