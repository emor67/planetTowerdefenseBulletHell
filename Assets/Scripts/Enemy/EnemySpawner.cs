using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    //References
    [SerializeField] private GameObject[] enemyPrefabs;

    //Variables
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemyPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float diffucultyScaleFactor = 1f;
    [SerializeField] private GameObject[] spawnPointObjects;

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
    }

    private void AddListeners()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        UpdateEnemyWave();
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
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies + Mathf.Pow(currentWave, diffucultyScaleFactor));
    }

    private void SpawnEnemy()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue >= 0.50f && randomValue <= 1.00f){
            randomIndex = 0;
        } else if(randomValue >= 0.20f && randomValue <= 0.50f){
            randomIndex = 1;
        } else if(randomValue >= 0f && randomValue <= 0.20f){
            randomIndex = 2;
        }
       GameObject prefabToSpawn = enemyPrefabs[randomIndex];
       Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
}
