using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("enemy"))
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = other.GetComponent<EnemyPath>();
            enemyPath.KillTween();

            Destroy(other.gameObject);
        }
    }
}

