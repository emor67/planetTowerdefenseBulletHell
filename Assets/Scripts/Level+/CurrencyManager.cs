using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    //references
    [SerializeField] private TextMeshProUGUI coinsText;
    //variables
    [SerializeField] private int coins;
    [SerializeField] private int gem;

    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void RemoveCoins(int amount)
    {
        coins -= amount;
    }

    public void AddGems(int amount)
    {
        gem += amount;
    }
    public void RemoveGems(int amount)
    {
        gem -= amount;
    }

    void Update()
    {
        coinsText.text = "Coins: " + coins.ToString();
    }
}
