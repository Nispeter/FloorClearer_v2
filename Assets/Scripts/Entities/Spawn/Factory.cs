using UnityEngine;

public abstract class Factory : MonoBehaviour
{
    protected T InstantiatePrefab<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : Object
    {
        GameObject obj = Instantiate(prefab, position, rotation);
        if (typeof(T) == typeof(GameObject))
        {
            return obj as T;
        }
        return obj.GetComponent<T>();
    }
}

