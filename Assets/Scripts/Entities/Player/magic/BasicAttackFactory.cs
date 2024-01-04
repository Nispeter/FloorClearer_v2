using UnityEngine;

public class BasicAttackFactory : Factory
{
    public GameObject BasicAttackPrefab;
    public float basicAttackForce = 30f;
    public float basicShootRate = 0.2f;

    [SerializeField] private float _basicShootTime = 0f;

    public void Setup()
    {
        _basicShootTime = 0f;
    }

    public void CreateBasicAttack(Transform aimPointer)
    {
        if (Time.time > _basicShootTime)
        {
            GameObject bullet = InstantiatePrefab<GameObject>(BasicAttackPrefab, aimPointer.position, aimPointer.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward.normalized * basicAttackForce, ForceMode.Impulse);
            _basicShootTime = Time.time + basicShootRate;
        }
    }
}
