using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("enemy"))
        {
            EnemySpawner.onEnemyDestroyed.Invoke();
            Destroy(other.gameObject);
        }
    }
}

