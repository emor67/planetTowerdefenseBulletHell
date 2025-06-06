using UnityEngine;

public class Bullet : MonoBehaviour
{
    //references
    [SerializeField] private Rigidbody2D rb;

    //variables
    public float bulletSpeed = 10f;
    public float bulletDamage = 45f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

      private void Start(){
        Destroy(gameObject, 3f);
    }

    [System.Obsolete]
    private void FixedUpdate() {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);

        Destroy(gameObject);
    }
}
