using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public GameObject spellImagePrefab;
    public Transform spellImagesParent;
    public Sprite placeHolderSpell;

    private Queue<Spell> storedSpells = new Queue<Spell>();
    private List<Image> spellImages = new List<Image>();

    public void AddSpell(Spell spell)
    {
        storedSpells.Enqueue(spell);

        Image spellImage = Instantiate(spellImagePrefab, spellImagesParent).GetComponent<Image>();
        if (spell.thumbnail != null)
            spellImage.sprite = spell.thumbnail;
        else
            spellImage.sprite = placeHolderSpell;
        spellImages.Add(spellImage);
        UpdateUIPositions();
    }

    public void UseSpell()
    {
        if (storedSpells.Count > 0)
        {
            storedSpells.Dequeue();
            if (spellImages.Count > 0)
            {
                Destroy(spellImages[0].gameObject);
                spellImages.RemoveAt(0);
                UpdateUIPositions();
            }
        }
    }

    private void UpdateUIPositions()
    {
        float xOffset = 50f;
        float xPosition = 0f;
        float lerpSpeed = 10f;
        foreach (Image spellImage in spellImages)
        {
            Vector3 targetPosition = new Vector3(xPosition, 0f, 0f);
            spellImage.transform.localPosition = Vector3.Lerp(spellImage.transform.localPosition, targetPosition, lerpSpeed);
            xPosition += xOffset;
        }
    }
}
