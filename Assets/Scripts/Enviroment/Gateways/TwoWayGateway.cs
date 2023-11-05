using UnityEngine;

public class TwoWayGateway : Gateway
{
    public TwoWayGateway targetGateway;
    public Transform exitPoint;
    private Transform targetPosition;

    void Start()
    {
        targetPosition = targetGateway.exitPoint;
    }

    public override void Transfer(GameObject player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (characterController != null)
        {
            Vector3 offset = targetPosition.position - player.transform.position;
            characterController.Move(offset);

            player.transform.rotation = targetPosition.rotation;
        }
        else
        {
            player.transform.position = targetPosition.position;
            player.transform.rotation = targetPosition.rotation;
        }
    }

}
