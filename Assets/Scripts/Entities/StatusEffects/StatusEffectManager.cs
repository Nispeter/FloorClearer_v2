using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void ApplyEffect(StatusEffect effect)
    {
        activeEffects.Add(effect);
        StartCoroutine(HandleEffect(effect));
    }

    private IEnumerator HandleEffect(StatusEffect effect)
    {
        effect.Apply(this.gameObject);

        yield return new WaitForSeconds(effect.Duration);

        effect.Remove(this.gameObject);
        activeEffects.Remove(effect);
    }
}
