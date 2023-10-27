using UnityEngine;

public class PointGateway : Gateway
{
    public Transform targetPosition;

    public override void Transfer(GameObject player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (characterController != null)
        {
            Vector3 offset = targetPosition.position - transform.position;
            characterController.Move(offset);

            player.transform.rotation = targetPosition.rotation;
        }
    }
}
