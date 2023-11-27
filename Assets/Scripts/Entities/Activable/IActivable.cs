using System.Collections;
using UnityEngine;

public interface IActivable
{
    void Activate();
    IEnumerator DeactivateAfterDelay(float delay);
}