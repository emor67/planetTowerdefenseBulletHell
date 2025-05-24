using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Transform firingPoint;

    public AudioSource audioSource;
    public AudioClip shootSound; 

    public SpaceShipBuy spaceShipBuy;

    private Camera mainCamera;

    [System.Obsolete]
    void Start()
    {
        mainCamera = Camera.main;
        spaceShipBuy = FindObjectOfType<SpaceShipBuy>();
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
            audioSource.PlayOneShot(shootSound);
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - firingPoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemySpawner.onEnemyDestroyed.Invoke();

            EnemyPath enemyPath = other.GetComponent<EnemyPath>();

            enemyPath.KillTween();

            Destroy(other.gameObject);

            Destroy(this.gameObject);

            spaceShipBuy.isAlive = false;
        }
    }
    

}
