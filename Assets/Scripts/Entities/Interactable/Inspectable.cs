using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : MonoBehaviour, IInteractable
{
    public int itemCount = 0;
    public string interactHint {get; set;}
    public void Start(){
      interactHint = "Inspect";
    }

    public void Interact()
    {
        Debug.Log($"Storage has {itemCount} items.");
    }
}
