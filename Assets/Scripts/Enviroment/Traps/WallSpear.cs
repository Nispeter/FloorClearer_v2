using System.Collections;
using UnityEngine;
/*
REFACTORIZAR UNA FUNCION ABSTRACTA DE MELEE ATTACK QUE HEREDE DE IATTACK!!  
*/
public class WallSpear : MonoBehaviour, IActivable, IAttack
{
    [Header("Status")]
    public float deactivateDelay = 5f;

    [Header("Lerp")]
    public float lerpTime = 0.1f;
    public Vector3 activePositionOffset = new Vector3(0, 2f, 0f);
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    public float spearDamage = 10f;
    public Collider damageCollider;
    private float colliderTime = 0.5f;

    private void Start()
    {
        damageCollider = GetComponent<Collider>();
        originalPosition = transform.position;
    }

    public IEnumerator ActivateDamageCollider(float deactivateDelay){
        damageCollider.enabled = true; 
        yield return new WaitForSeconds(deactivateDelay); 
        damageCollider.enabled = false; 
    }

      public void DealDamage(IHealth target)
    {
        if (target != null)
        {
            target.TakeDamage(spearDamage);
        }
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        IHealth target = collision.gameObject.GetComponent<IHealth>();
        DealDamage(target);
    }


    public void Activate()
    {
        targetPosition = transform.position + transform.TransformDirection(activePositionOffset);
        StartCoroutine(MoveSpear(targetPosition, lerpTime));
        StartCoroutine(ActivateDamageCollider(colliderTime));
        StartCoroutine(DeactivateAfterDelay(deactivateDelay));
    }

    private IEnumerator MoveSpear(Vector3 targetPosition, float duration)
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

    public IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetPosition = originalPosition;
        StartCoroutine(MoveSpear(targetPosition, lerpTime * 2));
    }
}
