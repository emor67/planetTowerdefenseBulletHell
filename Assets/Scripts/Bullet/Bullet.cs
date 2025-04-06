using UnityEngine;

public class Bullet : MonoBehaviour
{
    //references
    [SerializeField] private Rigidbody2D rb;

    //variables
    [SerializeField] private float bulletSpeed = 10f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate() {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
