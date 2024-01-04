using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterSpawner : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private int pay = 50;
    [SerializeField] private int coins = 0;
    [SerializeField] private int coinsIncreaseAmount = 2;
    private int minCost = 2147483647;
    [Header("Time")]
    [SerializeField] private float timeSinceLastAction = 0f;
    [SerializeField] private float actionInterval = 15f;
    [Header("Params")]
    [SerializeField] private int saveProbability = 50;
    [SerializeField] private float spawnRadius = 100f;
     private float minimumSpawnRadius = 0.1f;
    [SerializeField] private float clusterRadius = 20f;

    [SerializeField] private Player player;
    [SerializeField] private List<GameObject> Enemies;
    private List<GameObject> EnemiesToSpawn = new List<GameObject>();
    private TimeManager timeManager;

    void Start()
    {
        timeManager = InGameManager.Instance.timeManager;
        checkMinCost();
    }

    void Update()
    {
        if (!timeManager.isGamePaused)
        {
            timeSinceLastAction += Time.deltaTime;

            if (timeSinceLastAction >= actionInterval)
            {
                coins += pay;
                pay += coinsIncreaseAmount;
                TakeAction();
                timeSinceLastAction = 0f;
            }
        }
    }

    private void checkMinCost(){
        foreach (GameObject enemy in Enemies){
            int enemyCost = enemy.GetComponent<Enemy>().cost;
            if(minCost > enemyCost)
                minCost = enemyCost;
        }
    }
    private void TakeAction()
    {
        int saveTry = Random.Range(1, 100);
        if (saveTry <= saveProbability)
        {
            return;         //Save coins
        }
        SpendCoins();
    }

    private void SpendCoins()
    {
        EnemiesToSpawn.Clear();
        int enemiesListSize = Enemies.Count;
        while (coins > 0 && minCost < coins)
        {
            int toGenEnemyNum = Random.Range(0, enemiesListSize-1);
            GameObject enemy = Enemies[toGenEnemyNum];
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript.cost <= coins)
            {
                coins -= enemyScript.cost;
                EnemiesToSpawn.Add(enemy);
            }
        }
        EnemiesToSpawn.Sort((a, b) => a.name.CompareTo(b.name));
        SpawnEnemies();
    }

    private Vector3 FlattenVector(Vector3 vector)
    {
        if(vector.x < minimumSpawnRadius) vector.x = minimumSpawnRadius;
        if(vector.z < minimumSpawnRadius) vector.z = minimumSpawnRadius;
        return new Vector3(vector.x, 0, vector.z);
    }
    private void SpawnEnemies()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 clusterCenter = Vector3.zero;
        string lastEnemyName = "";
        Debug.Log("Spawning enemies...");

        foreach (GameObject enemy in EnemiesToSpawn)
        {
            if (enemy.name != lastEnemyName)
            {
                clusterCenter = playerPosition + FlattenVector(Random.insideUnitSphere) * spawnRadius;
                lastEnemyName = enemy.name;
            }
            Vector3 spawnPosition = clusterCenter + FlattenVector(Random.insideUnitSphere) * clusterRadius;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }
}


