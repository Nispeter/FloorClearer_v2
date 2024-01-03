using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMasterSpawner : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private int pay = 50;
    [SerializeField] private int coins = 0;
    [SerializeField] private int coinsIncreaseAmount = 2;
    [Header("Time")]
    [SerializeField] private float timeSinceLastAction = 0f;
    [SerializeField] private float actionInterval = 5f;
    [Header("Params")]
    [SerializeField] private int saveProbability = 70;
    [SerializeField] private float spawnRadius = 20f;
    [SerializeField] private float clusterRadius = 5f;

    [SerializeField] private Player player;
    [SerializeField] private List<GameObject> Enemies;
    private List<GameObject> EnemiesToSpawn;
    private TimeManager timeManager;

    void Start()
    {
        timeManager = InGameManager.Instance.timeManager;
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

    private void TakeAction()
    {
        int saveTry = Random.Range(1, saveProbability);
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
        while (coins > 0)
        {
            int toGenEnemyNum = Random.Range(0, enemiesListSize);
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

    private void SpawnEnemies()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 clusterCenter = Vector3.zero;
        string lastEnemyName = "";
        

        foreach (GameObject enemy in EnemiesToSpawn)
        {
            if (enemy.name != lastEnemyName)
            {
                clusterCenter = playerPosition + Random.insideUnitSphere * spawnRadius;
                lastEnemyName = enemy.name;
            }
            Vector3 spawnPosition = clusterCenter + Random.insideUnitSphere * clusterRadius;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }

}



