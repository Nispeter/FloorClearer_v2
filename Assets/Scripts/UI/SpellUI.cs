using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public GameObject spellImagePrefab; 
    public Transform spellImagesParent; 
    public Sprite placeHolderSpell; 
    public int maxSpellSlots = 3; 
    private Queue<Spell> storedSpells = new Queue<Spell>();
    public List<Image> spellImages = new List<Image>();

    private void Start()
    {
        //InitializeSpellSlots();
        UpdateSpellUI();
    }

    private void InitializeSpellSlots()
{
    spellImages.Clear(); 

    foreach (Transform child in spellImagesParent)
    {
        Image image = child.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = placeHolderSpell; 
            spellImages.Add(image); 
        }
    }

}

    public void AddSpell(Spell spell)
    {
        storedSpells.Enqueue(spell);
        UpdateSpellUI();
    }

    public void UseSpell()
    {
        if (storedSpells.Count > 0)
        {
            storedSpells.Dequeue();
            UpdateSpellUI();
        }
    }

    private void UpdateSpellUI()
    {
        int index = 0;
        foreach (var spell in storedSpells)
        {
            if (index < spellImages.Count)
            {
                spellImages[index].sprite = spell.thumbnail != null ? spell.thumbnail : placeHolderSpell;

            }
            index++;
        }

        // Update or deactivate remaining slots
        for (int i = index; i < spellImages.Count; i++)
        {
            spellImages[i].sprite = placeHolderSpell; 
            
        }
    }
}
