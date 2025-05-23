using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    //references
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemText;
    //variables
    public int coins;
    public int gem;

    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void RemoveCoins(int amount)
    {
        if (coins - amount < 0)
        {
            Debug.Log("Not enough coins!");
            return;
        }
        else coins -= amount;
    }

    public void AddGems(int amount)
    {
        gem += amount;
    }
    public void RemoveGems(int amount)
    {
        if (gem - amount < 0)
        {
            Debug.Log("Not enough gems!");
            return;
        }
        else gem -= amount;
    }

    void Update()
    {
        coinsText.text = "Coins: " + coins.ToString();
        gemText.text = "Gems: " + gem.ToString();
    }
}
