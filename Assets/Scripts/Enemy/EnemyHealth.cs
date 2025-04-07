using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //variables
    [SerializeField] private float enemyHealth = 100f;
    public float enemyDamage = 100f;
    [SerializeField] private int coinValue;
    [SerializeField] private GameObject levelManagerObject;

    void Start()
    {
        levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = GetComponent<EnemyPath>();
            enemyPath.KillTween();

            levelManagerObject.GetComponent<CurrencyManager>().AddCoins(coinValue);

            Destroy(gameObject);
        }
    }
}
