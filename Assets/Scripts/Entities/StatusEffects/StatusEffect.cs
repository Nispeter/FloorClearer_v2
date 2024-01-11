using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public enum EffectType { Poison, Freeze, Burn, SpeedBoost }
    public EffectType effectType;
    public float duration;
    public float intensity;

    // Constructor
    public StatusEffect(EffectType type, float duration, float intensity)
    {
        this.effectType = type;
        this.duration = duration;
        this.intensity = intensity;
    }

    // Método para aplicar el efecto (se puede personalizar para cada tipo)
    public void ApplyEffect(IEntity character)
    {
        // Implementar lógica específica del efecto aquí
    }
}
