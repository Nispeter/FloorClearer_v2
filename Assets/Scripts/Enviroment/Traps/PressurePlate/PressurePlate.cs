using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IActivable
{
    [Header("Status")]
    public float deactivateTime = 5f;
    private bool isActivated = false;

    [Header("Activable target")]
    public GameObject targetObj;
    private IActivable target;

    [Header("Lerp")]
    public float lerpTime = 1f;
    public Vector3 activePositionOffset = new Vector3(0, -0.05f, 0);
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    public void Start()
    {
        target = targetObj.GetComponent<IActivable>();
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Activate();
    }

    public void Activate()
    {
        if (!isActivated)
        {
            isActivated = true;
            targetPosition = originalPosition + activePositionOffset;
            StartCoroutine(MovePlate(targetPosition, lerpTime));
            target.Activate();
            StartCoroutine(DeactivateAfterDelay(deactivateTime));
        }
    }

    public IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isActivated = false;
        targetPosition = originalPosition;
        StartCoroutine(MovePlate(targetPosition, lerpTime*2));
    }

    private IEnumerator MovePlate(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
