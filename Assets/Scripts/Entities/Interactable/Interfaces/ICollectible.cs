using UnityEngine;
public interface ICollectible: IInteractable {
    public void OnTriggerEnter(Collider other){}
}