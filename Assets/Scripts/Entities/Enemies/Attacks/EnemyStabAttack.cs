using System.Collections;
using UnityEngine;

public class EnemyStabAttack : MonoBehaviour, IAttack
{
    [SerializeField] private Collider _damageCollider;
    [SerializeField] private float _damage;

    public Collider damageCollider
    {
        get { return _damageCollider; }
        set { _damageCollider = value; }
    }

    public float damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void Start()
    {
        if (_damageCollider != null)
        {
            _damageCollider.enabled = false;
        }
    }

    public IEnumerator ActivateDamageCollider(float delayBeforeActivation, float durationOfActivation)
    {

        yield return new WaitForSeconds(delayBeforeActivation);
        _damageCollider.enabled = true;
        yield return new WaitForSeconds(durationOfActivation);
        _damageCollider.enabled = false;
    }

    public void DealDamage(IHealth target)
    {
        if (target != null)
        {
            target.TakeDamage(_damage);
        }
    }

    public void PerformAttack(float delayBeforeActivation, float durationOfActivation)
    {
        StartCoroutine(ActivateDamageCollider(delayBeforeActivation, durationOfActivation));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_damageCollider.enabled)
        {
            IHealth target = other.GetComponent<IHealth>();
            DealDamage(target);
        }
    }
}