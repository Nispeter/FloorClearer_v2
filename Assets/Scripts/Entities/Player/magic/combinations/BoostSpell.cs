using System.Collections;
using UnityEngine;

public class BoostSpell : Spell
{
    public float boostDuration = 5f;

    public override void CastSpell(Transform cameraTransform)
    {
        BoostCoroutine();
    }

    private void BoostCoroutine()
    {
        StartCoroutine(InGameManager.Instance.inputController.ActivateBoost(boostDuration));
    }
}
