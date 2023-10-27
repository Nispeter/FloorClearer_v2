using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3.0f;
    public Sprite defaultCrosshair;
    public Sprite interactableCrosshair;

    public Image crosshairImage;
    public UIHintController uiHintController;

    private int _framesBetweenChecks = 50;
    private int _currentFrame = 0;

    private void Update()
    {
        _currentFrame++;
        if (_currentFrame >= _framesBetweenChecks)
        {
            CheckCursorChange();
            _currentFrame = 0;
        }
    }

   void CheckCursorChange()
{
    Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 2f);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, interactDistance))
    {
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
            UpdateCrosshair(interactableCrosshair);
            uiHintController.ShowHint();
            return;
        }
    }

    UpdateCrosshair(defaultCrosshair);
    uiHintController.HideHint();
}



    public void UpdateCrosshair(Sprite crosshairSprite)
    {
        crosshairImage.sprite = crosshairSprite;
    }
}
