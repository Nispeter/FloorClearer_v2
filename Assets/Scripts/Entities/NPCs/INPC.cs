using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;

public class INPC : MonoBehaviour, IInteractable, IEntity
{
    public string interactHint { get; set; }

    // Reference to DialogueSystemTrigger
    private DialogueSystemTrigger dialogueSystem;

    private void Awake()
    {
        // Get the DialogueSystemTrigger component attached to this GameObject
        dialogueSystem = GetComponent<DialogueSystemTrigger>();
        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystemTrigger component not found on this NPC.");
        }
    }

    private void Start()
    {
        interactHint = "Speak";
    }

    public void ModifyMovementSpeed(float modifier)
    {
        Debug.Log("Slowed");
    }

    public void Interact()
    {
        Debug.Log("Speaking with someone");

        // Trigger the dialogue if DialogueSystemTrigger is found
        if (dialogueSystem != null)
        {
            dialogueSystem.OnUse();
        }
    }
}
