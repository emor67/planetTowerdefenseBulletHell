using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //variables
    [SerializeField] private float enemyHealth = 100f;

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = GetComponent<EnemyPath>();
            enemyPath.KillTween();

            Destroy(gameObject);
        }
    }
}
