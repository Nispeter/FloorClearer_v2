using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Factory
{
    [SerializeField] private GameObject baseEnemyPrefab;

    public Enemy CreateBaseEnemy(Vector3 position, Quaternion rotation)
    {
        return InstantiatePrefab<Enemy>(baseEnemyPrefab, position, rotation);
    }
}

