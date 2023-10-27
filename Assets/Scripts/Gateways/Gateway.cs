using UnityEngine;

public abstract class Gateway : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { Debug.Log("Trigger entered by: " + other.name);
        if (other.CompareTag("Player")) 
        {
            Transfer(other.gameObject);
        }
    }

    public abstract void Transfer(GameObject player);
}
