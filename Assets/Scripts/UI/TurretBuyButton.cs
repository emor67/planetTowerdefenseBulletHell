using UnityEngine;
using TMPro;
using NUnit.Framework;

public class TurretBuyButton : MonoBehaviour
{
    //references
    [SerializeField] private GameObject automaticTurretPrefab;
    [SerializeField] private GameObject sniperTurretPrefab;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private TextMeshProUGUI automaticTurretCostText;
    [SerializeField] private TextMeshProUGUI sniperTurretCostText;

    [SerializeField] private GameObject colorAutomaticTurretPrefab;
    [SerializeField] private GameObject colorSniperTurretPrefab;


    //variables
    [SerializeField] private int automaticTurretCost;
    [SerializeField] private int sniperTurretCost;

    private bool isAutomaticTurretActive = false;
    private bool isSniperTurretActive = false;

    void Start()
    {
        automaticTurretCostText.text = automaticTurretCost.ToString();
        sniperTurretCostText.text = sniperTurretCost.ToString();

        colorAutomaticTurretPrefab.SetActive(false);
        colorSniperTurretPrefab.SetActive(false);

        automaticTurretPrefab.SetActive(false);
        sniperTurretPrefab.SetActive(false);
    }
    public void BuyAutomaticTurret(){
        if((currencyManager.coins >= automaticTurretCost) && !isAutomaticTurretActive){
            
            currencyManager.RemoveCoins(automaticTurretCost);
            
            automaticTurretPrefab.SetActive(true);
            sniperTurretPrefab.SetActive(false);

            colorAutomaticTurretPrefab.SetActive(true);
            colorSniperTurretPrefab.SetActive(false);
        }

        isAutomaticTurretActive = true;
        isSniperTurretActive = false;
    }
    public void BuySniperTurret(){
        if((currencyManager.coins >= sniperTurretCost) && !isSniperTurretActive){
            
            currencyManager.RemoveCoins(sniperTurretCost);
            
            sniperTurretPrefab.SetActive(true);
            automaticTurretPrefab.SetActive(false);

            colorSniperTurretPrefab.SetActive(true);
            colorAutomaticTurretPrefab.SetActive(false);
        }
        isSniperTurretActive = true;
        isAutomaticTurretActive = false;
    }
}
