using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Microlight.MicroBar;
using TMPro;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    //References
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] MicroBar waveProgressBar;
    [SerializeField] private GameObject[] spawnPointObjects;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;

    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject[] otherMenus;

    public Transform target;

    public CurrencyManager currencyManager;

    //Variables
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemyPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float diffucultyScaleFactor = 1f;
    [SerializeField] private float diffucultyScaleFactorEnemy = 1f;

    [SerializeField] private GameObject[] pile1;
    [SerializeField] private GameObject[] pile2;
    [SerializeField] private GameObject[] pile3;


    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private Transform spawnPoint;
    private int randomIndex;

    //Events
    public static UnityEvent onEnemyDestroyed = new UnityEvent();

    private void Awake()
    {
        AddListeners();

        upgradeMenu.SetActive(false);
    }

    private void AddListeners()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        waveText.text = "Wave 1";

        DisableaWholePile();

        enemy1 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy1.prefab", typeof(GameObject)) as GameObject;
        enemy2 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy2.prefab", typeof(GameObject)) as GameObject;
        enemy3 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy3.prefab", typeof(GameObject)) as GameObject;
    }

    private void Update()
    {
        UpdateEnemyWave();
        //enable coins and gems texts
        currencyManager.coinsText.gameObject.SetActive(true);
        currencyManager.gemText.gameObject.SetActive(true);
    }

    private void UpdateEnemyWave()
    {
        if (!isSpawning) return;

        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= (1f / enemyPerSecond) && enemiesLeftToSpawn > 0)
            {
                SpawnPointSetter();
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0f;
            }
        }

        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

    private void SpawnPointSetter()
    {
        spawnPoint = spawnPointObjects[Random.Range(0, spawnPointObjects.Length)].transform;
    }

    private IEnumerator StartWave()
    {
        if (enemiesAlive <= 0)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            isSpawning = true;
            enemiesLeftToSpawn = EnemiesPerWave();
            waveProgressBar.Initialize(enemiesLeftToSpawn);
            if (currentWave > 1)
            {
                IncreaseEnemyStats();
            }

            StartCoroutine(UpgradeMenu());
        }
       
    }

    private IEnumerator UpgradeMenu()
    {

        if (currentWave % 4 == 0)
        {
            upgradeMenu.SetActive(true);
            Time.timeScale = 0f;

            foreach (GameObject menu in otherMenus)
            {
                menu.SetActive(false);
            }

            EnableCardFromPile();

            yield return new WaitForSeconds(0.1f); // Small delay to ensure UI updates
        }
    }

    //Buttons
    public void UpgradeMenuClose()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;

        foreach (GameObject menu in otherMenus)
        {
            menu.SetActive(true);
        }
        DisableaWholePile();
    }
    //Buttons

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies + Mathf.Pow(currentWave, diffucultyScaleFactor));
    }

    private void SpawnEnemy()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue >= 0.50f && randomValue <= 1.00f)
        {
            randomIndex = 0;
        }
        else if (randomValue >= 0.20f && randomValue <= 0.50f)
        {
            randomIndex = 1;
        }
        else if (randomValue >= 0f && randomValue <= 0.20f)
        {
            randomIndex = 2;
        }

        GameObject prefabToSpawn = enemyPrefabs[randomIndex];

        // Instantiate the enemy at spawn point
        GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        // Rotate the spawned enemy to look at the target
        Vector3 direction = (target.position - spawnedEnemy.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 scale = spawnedEnemy.transform.localScale;
        if (target.position.x < spawnedEnemy.transform.position.x && target.position.y < spawnedEnemy.transform.position.y)
        {
            scale.x = -Mathf.Abs(scale.x);
            scale.y = -Mathf.Abs(scale.y);
        }
        else if (target.position.x < spawnedEnemy.transform.position.x && target.position.y > spawnedEnemy.transform.position.y)
        {
            scale.x = -Mathf.Abs(scale.x);
            scale.y = -Mathf.Abs(scale.y);
        }
        else if (target.position.x > spawnedEnemy.transform.position.x && target.position.y < spawnedEnemy.transform.position.y)
        {
            scale.x = -Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
        }
        else if (target.position.x > spawnedEnemy.transform.position.x && target.position.y > spawnedEnemy.transform.position.y)
        {
            scale.x = -Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
        }

        spawnedEnemy.transform.localScale = scale;

        spawnedEnemy.transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        waveProgressBar.UpdateBar(waveProgressBar.CurrentValue - 1);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        currencyManager.AddGems(1);

        waveText.text = "Wave " + currentWave.ToString();

        StartCoroutine(StartWave());
    }

    private void IncreaseEnemyStats()
    {
        enemy1.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy1.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        //enemy1.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);

        enemy2.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy2.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        //enemy2.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);

        enemy3.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy3.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        //enemy3.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
    }

    private void DisableaWholePile()
    {
        //disable all piles
        foreach (GameObject card in pile1)
        {
            card.SetActive(false);
        }
        foreach (GameObject card in pile2)
        {
            card.SetActive(false);
        }
        foreach (GameObject card in pile3)
        {
            card.SetActive(false);
        }
    }
    
    private void EnableCardFromPile()
    {
        //set 1 random card from pile1 to active
        int randomIndex1 = Random.Range(0, pile1.Length);
        pile1[randomIndex1].SetActive(true);

        //set 1 random card from pile2 to active
        int randomIndex2 = Random.Range(0, pile2.Length);
        pile2[randomIndex2].SetActive(true);

        //set 1 random card from pile3 to active
        int randomIndex3 = Random.Range(0, pile3.Length);
        pile3[randomIndex3].SetActive(true);
        
    }
}
