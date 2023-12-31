using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    public float damage;
    public float manaCost;
    public float cooldown;
    public float healing;
    public float castSpeed;
    public float spellSpeed;
    public float duration;

    public Sprite thumbnail;
    public List<string> damageTypes  = new List<string>();

    virtual public void CastSpell(Transform cameraTransform){}
}
