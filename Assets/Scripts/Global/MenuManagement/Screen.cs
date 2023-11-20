using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject screen;

    public void SetScreen(GameObject screenObj){
        screen = screenObj;
    }

    public void ActivateScreen()
    {
        if (screen != null)
        {
            screen.SetActive(true);
        }
        else {Debug.LogError("Screen is Null");}
    }

    public void DeactivateScreen()
    {
        if (screen != null)
        {
            screen.SetActive(false);
        }
    }
}
