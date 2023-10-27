using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardSpell : Spell
{
    [SerializeField] private Transform IceShardPrefab;

    public IceShardSpell()
    {
        damage = 1;
        manaCost = 1;
        cooldown = 1;
        healing = 0;
        castSpeed = 1;
        spellSpeed = 1;
        statusEffects.Add("cold");
    }

     public override void CastSpell(Transform cameraTransform)
    {
        Debug.Log(cameraTransform.forward);
        
        // Calculate the direction the camera is facing
        Vector3 cameraDirection = cameraTransform.forward.normalized;

        // Instantiate IceShardPrefab at the camera's position with the calculated direction
        Transform IceShardTransform = Instantiate(IceShardPrefab, cameraTransform.position, Quaternion.identity);
        // Pass the camera direction as a Vector3 to the Setup method
        IceShardTransform.GetComponent<IceShard>().Setup(cameraTransform, damage);
    }
}
