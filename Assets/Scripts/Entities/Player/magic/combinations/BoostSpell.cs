using System.Collections;
using UnityEngine;

public class BoostSpell : Spell
{

    void Start(){
        duration = 5f;
    }
    

    public override void CastSpell(Transform cameraTransform)
    {
        Boost();
    }

    private void Boost()
    {
        InGameManager.Instance.inputController.MovementBoost(duration);
    }
}
