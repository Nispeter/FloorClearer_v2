
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHintController : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public string pickupKey = "F";

    public void ShowHint()
    {
        hintText.text = $"[{pickupKey}] interact";
        hintText.enabled = true;
    }

    public void HideHint()
    {
        hintText.enabled = false;
    }
}