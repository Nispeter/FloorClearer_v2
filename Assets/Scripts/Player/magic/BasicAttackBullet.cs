using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBullet : PlayerProjectile
{
    public float lifetime = 30f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
