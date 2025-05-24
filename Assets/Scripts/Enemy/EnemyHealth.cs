using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
// variables
public float enemyHealth = 100f;
public float enemyDamage = 100f;
public int coinValue;

// references
[SerializeField] private GameObject levelManagerObject;
[SerializeField] private GameObject bloodEffectPrefab;
[SerializeField] private SpriteRenderer spriteRenderer; // Assign in Inspector

[Header("Damage Flash Settings")]
[SerializeField] private Color flashColor = new Color(1f, 0.3f, 0.3f); // Reddish tint
[SerializeField] private float flashDuration = 0.1f;

    private Color originalColor;

    public AudioSource audioSource;
    public AudioClip hurtSound;

    void Start()
{
    levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
    if (spriteRenderer == null)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    originalColor = spriteRenderer.color;
}

public void TakeDamage(float damage)
{
    enemyHealth -= damage;
    
    audioSource.PlayOneShot(hurtSound);

    // Start visual flash feedback
        StartCoroutine(DamageFlash());

    if (enemyHealth <= 0)
    {
        Instantiate(bloodEffectPrefab, transform.position, transform.rotation);

        EnemySpawner.onEnemyDestroyed.Invoke();

        EnemyPath enemyPath = GetComponent<EnemyPath>();
        enemyPath.KillTween();

        levelManagerObject.GetComponent<CurrencyManager>().AddCoins(coinValue);

        Destroy(gameObject);
    }
}

private IEnumerator DamageFlash()
{
    spriteRenderer.color = flashColor;
    yield return new WaitForSeconds(flashDuration);
    spriteRenderer.color = originalColor;
}
}
