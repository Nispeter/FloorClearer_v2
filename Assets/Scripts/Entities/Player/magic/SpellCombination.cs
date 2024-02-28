using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellEntry
{
    public int spellID;
    public Spell spell;
}
public class SpellCombination : MonoBehaviour
{
      [SerializeField] 
    private List<SpellEntry> spellEntries = new List<SpellEntry>();

    private Dictionary<int, Spell> knownSpells = new Dictionary<int, Spell>();

    private void Awake()
    {
        // Populate the dictionary from the serialized list
        foreach (SpellEntry entry in spellEntries)
        {
            knownSpells[entry.spellID] = entry.spell;
        }
    }


     public Spell Combine(List<ElementBall> elementBalls)
    {
        if (elementBalls.Count != 3)
        {
            Debug.Log("Not enough element balls for combination.");
            return null; // Return null if there aren't enough balls
        }

        int spellID = CalculateSpellID(elementBalls);
        return GetCombinedSpell(spellID);
    }

    private int CalculateSpellID(List<ElementBall> elementBalls)
    {
        int spellID = 0;
        foreach (var ball in elementBalls)
        {
            switch (ball.elementType)
            {
                case Element.Light:
                    spellID += 1;
                    break;
                case Element.Arcane:
                    spellID += 10;
                    break;
                case Element.Spirit:
                    spellID += 100;
                    break;
            }
        }
        return spellID;
    }
    private Spell GetCombinedSpell(int spellID)
    {
        Debug.Log("Combined Spell ID: " + spellID);
        Spell spell;
        if (knownSpells.TryGetValue(spellID, out spell))
        {
            return spell;
        }
        else
        {
            Debug.Log("Spell not found for ID: " + spellID);
            knownSpells.TryGetValue(3, out spell);
            return spell;   
        }
    }
}
