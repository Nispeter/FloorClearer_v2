using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardFactory
{
    private GameObject iceShardPrefab;

    public IceShardFactory(GameObject prefab)
    {
        iceShardPrefab = prefab;
    }

    public IceShard CreateIceShard(Transform cam, float damage)
    {
        GameObject iceShardObject = GameObject.Instantiate(iceShardPrefab, cam.position, Quaternion.identity);
        IceShard iceShard = iceShardObject.GetComponent<IceShard>();
        
        if (iceShard != null)
        {
            iceShard.Setup(cam, damage);
            return iceShard;
        }
        else
        {
            // Handle the case where the instantiated object doesn't have an IceShard component.
            Debug.LogError("IceShard component not found on the instantiated object.");
            GameObject.Destroy(iceShardObject);
            return null;
        }
    }
}