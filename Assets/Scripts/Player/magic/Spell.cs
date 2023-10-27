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

    public Sprite thumbnail;
    public List<string> statusEffects  = new List<string>();

    virtual public void CastSpell(Transform cameraTransform){}
}
