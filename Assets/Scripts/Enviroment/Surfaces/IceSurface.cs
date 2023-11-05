using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSurface : MonoBehaviour, ISurface
{
    private float lifetime;

    public void OnEntityEnter(IEntity entity)
    {
        entity.ModifyMovementSpeed(-0.5f);
    }

    public void OnEntityStay(IEntity entity)
    {
    }

    public void OnEntityExit(IEntity entity)
    {
        entity.ModifyMovementSpeed(0.5f);
    }

    public void SnapToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            transform.position = hit.point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            OnEntityEnter(entity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            OnEntityStay(entity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            OnEntityExit(entity);
        }
    }

    private void Start()
    {
        SnapToGround();
        if (lifetime > 0) Destroy(gameObject, lifetime);
    }

    public void SetLifetime(float time)
    {
        lifetime = time;
    }
}