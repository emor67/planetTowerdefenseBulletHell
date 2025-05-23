using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Transform firingPoint;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Move();
        Aim();
        Shoot();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void Aim()
{
    Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = (mousePos - transform.position).normalized;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
}


void Shoot()
{
    if (Input.GetMouseButtonDown(0)) // Left click
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firingPoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }
}


}
