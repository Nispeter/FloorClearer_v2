using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public float Duration { get; private set; }

    public StatusEffect(float duration)
    {
        Duration = duration;
    }

    public virtual void Apply(GameObject entity)
    {

    }

    public virtual void Remove(GameObject entity)
    {

    }
}
