using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementalBallUpdate : MonoBehaviour
{
    public List<Image> elementalUIImages; 
    public Color lightElementColor = Color.yellow;
    public Color arcaneElementColor = Color.magenta;
    public Color spiritElementColor = Color.red;
    public Color defaultColor = Color.clear; 

    private void Start()
    {
        ClearColor();
    }

    private void ClearColor(){
        foreach (var uiImage in elementalUIImages)
        {
            uiImage.color = defaultColor;
        }
    }

    public void UpdateElementalUI(List<ElementBall> elementBalls)
    {
        ClearColor();

        for (int i = 0; i < elementBalls.Count; i++)
        {
            if (i < elementalUIImages.Count) 
            {
                var elementType = elementBalls[i].elementType;
                Color tempColor = GetColorForElement(elementType);
                tempColor.a = 0.5f;
                elementalUIImages[i].color = tempColor;
            }
        }
    }

    Color GetColorForElement(Element elementType)
    {
        switch (elementType)
        {
            case Element.Light:
                return lightElementColor;
            case Element.Arcane:
                return arcaneElementColor;
            case Element.Spirit:
                return spiritElementColor;
            default:
                return defaultColor;
        }
    }
}
