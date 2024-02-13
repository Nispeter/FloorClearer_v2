using System.Collections;
using UnityEngine;

public class EnemyStabAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private Collider _damageCollider;
    public Collider damageCollider
    {
        get => _damageCollider;
        set => _damageCollider = value;
    }

    [SerializeField]
    private float _damage = 2f;
    public float damage
    {
        get => _damage;
        set => _damage = value;
    }

    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        if (_damageCollider != null)
        {
            Debug.Log("collider acquired");
            _damageCollider.enabled = false;
        }
    }

    public IEnumerator ActivateDamageCollider(float delayBeforeActivation, float durationOfActivation)
    {
        //ACTIVATING OR DEACTIVATING DONT WORK!!
        if (_damageCollider == null) yield break;
        yield return new WaitForSeconds(delayBeforeActivation);
        _damageCollider.enabled = true; 
        yield return new WaitForSeconds(durationOfActivation);
        _damageCollider.enabled = false;
    }


    public void DealDamage(IHealth target)
    {
        target.TakeDamage(_damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an entity with IHealth
        if (other.TryGetComponent<IHealth>(out IHealth health))
        {
            DealDamage(health);
        }
    }
}
