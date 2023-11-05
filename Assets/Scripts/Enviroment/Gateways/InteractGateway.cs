using UnityEngine;

public class InteractGateway : Gateway, IInteractable
{
    public Transform targetPosition;
    private CharacterController playerController;
    public string interactHint {get; set;}
    private void Start()
    {
        interactHint = "Interact";
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController>();
        }
    }

    public void Interact()
    {
        if (playerController != null)
        {
            Vector3 offset = targetPosition.position - playerController.transform.position;
            playerController.Move(offset);
            playerController.transform.rotation = targetPosition.rotation;
        }
    }

    public override void Transfer(GameObject player)
    {

    }
}
