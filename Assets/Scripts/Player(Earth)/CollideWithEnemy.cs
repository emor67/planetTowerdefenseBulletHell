using Unity.VisualScripting;
using UnityEngine;

public class CollideWithEnemy : MonoBehaviour
{
    //references
    [SerializeField] private PlayerHealth playerHealth;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("enemy"))
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = other.GetComponent<EnemyPath>();

            playerHealth.TakeDamage(other.GetComponent<EnemyHealth>().enemyDamage);
            
            enemyPath.KillTween();

            Destroy(other.gameObject);
        }
    }
}

