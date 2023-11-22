using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpell : Spell
{
    public float boostDuration = 5f;

    public override void CastSpell(Transform cameraTransform)
    {
        StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        InGameManager.Instance.inputController.ActivateBoost();
        yield return new WaitForSeconds(boostDuration);
        InGameManager.Instance.inputController.DeactivateBoost();

        Destroy(gameObject);
    }
}
