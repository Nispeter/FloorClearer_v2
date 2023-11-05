using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
    public void ModifyMovementSpeed(float modifier)
    {
        Debug.Log("Slowed");
    }
}
