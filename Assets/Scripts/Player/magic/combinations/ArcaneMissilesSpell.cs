using UnityEngine;

public class ArcaneMissilesSpell : Spell // Assuming there's a Spell base class
{
    public GameObject crystalShardPrefab;
    public int numberOfShards = 3;
    public float shardSpawnDistance = 2f; 
    public float distanceBetweenShards = 0.5f;

    public override void CastSpell(Transform cameraTransform)
    {
        if (crystalShardPrefab == null)
        {
            Debug.LogWarning("CrystalShard prefab not set!");
            return;
        }

        Vector3 spawnDirection = cameraTransform.forward;
        Vector3 startPosition = cameraTransform.position + spawnDirection * shardSpawnDistance;

        for (int i = 0; i < numberOfShards; i++)
        {
            GameObject shard = Instantiate(crystalShardPrefab, startPosition + spawnDirection * i * distanceBetweenShards, Quaternion.identity);
            shard.GetComponent<CrystalShard>().Launch(cameraTransform, spawnDirection);
        }
    }
}
