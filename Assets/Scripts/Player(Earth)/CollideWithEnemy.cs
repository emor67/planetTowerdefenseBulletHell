using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class CollideWithEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private SpriteRenderer spriteRenderer; // Assign your player's SpriteRenderer
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.05f;
    [SerializeField] private Color damageColor = new Color(1f, 0.3f, 0.3f); // Reddish
    private Vector3 originalPosition;
    private Color originalColor;

    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = other.GetComponent<EnemyPath>();
            playerHealth.TakeDamage(other.GetComponent<EnemyHealth>().enemyDamage);
            enemyPath.KillTween();

            Destroy(other.gameObject);

            StartCoroutine(FeedbackEffect());
        }
    }

    private IEnumerator FeedbackEffect()
    {
        // Color flash
        spriteRenderer.color = damageColor;

        // Shake
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = originalPosition + new Vector3(offset.x, offset.y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        spriteRenderer.color = originalColor;
    }
}