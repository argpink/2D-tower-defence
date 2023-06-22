using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events; 

public class EnemySpawner : MonoBehaviour


{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float diffucultyScailingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy; 

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;


    private void Awake(){
        onEnemyDestroy.AddListener(EnemyDestroyed); 
    }

    private void Start(){
        StartWave();}

    private void Update()
    {
        if (!isSpawning) return;
         
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0){
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f; }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private void StartWave() {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave(); }

    private void SpawnEnemy() {
        GameObject prefabsToSpawn = enemyPrefabs[0];
        Instantiate(prefabsToSpawn, LevelManager.main.startPoint.position, Quaternion.identity); }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, diffucultyScailingFactor)); }
}


