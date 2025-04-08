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

    //Variables
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemyPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float diffucultyScaleFactor = 1f;
    [SerializeField] private float diffucultyScaleFactorEnemy = 1f;
    

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
        waveText.text = "Wave 1";

        enemy1 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy1.prefab", typeof(GameObject)) as GameObject;
        enemy2 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy2.prefab", typeof(GameObject)) as GameObject;
        enemy3 = AssetDatabase.LoadAssetAtPath("Assets/Art/Prefabs/enemy3.prefab", typeof(GameObject)) as GameObject;    
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
        waveProgressBar.Initialize(enemiesLeftToSpawn);
        if (currentWave > 1)
        {
            IncreaseEnemyStats();
        }

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
        waveProgressBar.UpdateBar(waveProgressBar.CurrentValue - 1);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        waveText.text = "Wave " + currentWave.ToString();
        StartCoroutine(StartWave());
    }

    private void IncreaseEnemyStats()
    {
        enemy1.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy1.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy1.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);

        enemy2.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy2.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy2.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);

        enemy3.GetComponent<EnemyHealth>().enemyHealth += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy3.GetComponent<EnemyHealth>().enemyDamage += Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
        enemy3.GetComponent<EnemyHealth>().coinValue += (int)Mathf.Pow(currentWave, diffucultyScaleFactorEnemy);
    }
}
