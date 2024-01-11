using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCasting : MonoBehaviour
{
    private const int _maxBallCombination = 3;
    private const int _maxStoredSpells = 2;

    public BasicAttackFactory basicAttackFactory;

    [Header("Spell Combination")]
    private SpellCombination MagicMixer;
    public Queue<Spell> storedSpells = new Queue<Spell>();
    public SpellUI SpellSlots;
    public Transform aimPointer;
    private Camera CasterCam;

    [Header("Element Orbs")]
    private List<ElementBall> elementBalls = new List<ElementBall>();
    public GameObject LightBall;
    public GameObject ArcaneBall;
    public GameObject SpiritBall;

    private void Start()
    {
        CasterCam = Camera.main;
        MagicMixer = GetComponent<SpellCombination>();
        basicAttackFactory.Setup();
    }

    public void CastSpellBasedOnKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha1:
                CreateElementBall(Element.Light);
                break;
            case KeyCode.Alpha2:
                CreateElementBall(Element.Arcane);
                break;
            case KeyCode.Alpha3:
                CreateElementBall(Element.Spirit);
                break;
            case KeyCode.R:
                DeleteElementBall();
                break;
            case KeyCode.E:
                if (elementBalls.Count == _maxBallCombination)
                    CombineElements();
                break;
        }
    }
    public void CastSpell()
    {
        if (storedSpells.Count > 0)
        {
            //Spell combinedSpell = storedSpells.Peek();
            Spell combinedSpell = storedSpells.Dequeue();
            SpellSlots.UseSpell();
            combinedSpell.CastSpell(CasterCam.transform);
        }
        else
        {
            basicAttackFactory.CreateBasicAttack(aimPointer);
        }
    }

    public void CombineElements()
    {
        if (elementBalls.Count == _maxBallCombination && storedSpells.Count < _maxStoredSpells)
        {
            Debug.Log("Combining...");
            for (int i = 0; i < _maxBallCombination; i++)
                Debug.Log(elementBalls[i].elementType);

            Spell tempSpell = MagicMixer.Combine(elementBalls);
            storedSpells.Enqueue(tempSpell);
            SpellSlots.AddSpell(tempSpell);

            foreach (ElementBall ball in elementBalls)
            {
                Destroy(ball.gameObject);
            }
            elementBalls.Clear();

        }
        else
        {
            Debug.Log("not enough elements");
            return;
        }
    }

    private void CreateElementBall(Element elementType)
    {
        if (elementBalls.Count == _maxBallCombination)
        {
            Destroy(elementBalls[elementBalls.Count - 1].gameObject);
            elementBalls.RemoveAt(elementBalls.Count - 1);
        }
        GameObject newBall = null;
        // Instantiate a new ElementBall prefab
        if (elementType == Element.Light)
        {
            newBall = Instantiate(LightBall, transform.position, Quaternion.identity);
        }
        else if (elementType == Element.Arcane)
        {
            newBall = Instantiate(ArcaneBall, transform.position, Quaternion.identity);
        }
        else if (elementType == Element.Spirit)
        {
            newBall = Instantiate(SpiritBall, transform.position, Quaternion.identity);
        }

        foreach (ElementBall ball in elementBalls)
        {
            ball.ballNumber += 1;
        }

        ElementBall ballScript = newBall.GetComponent<ElementBall>();
        ballScript.elementType = elementType;

        ballScript.caster = CasterCam;
        ballScript.ballNumber = 0;
        elementBalls.Insert(0, ballScript);
    }

    private void DeleteElementBall()
    {
        if (elementBalls.Count > 0)
        {
            ElementBall ballToDelete = elementBalls[elementBalls.Count - 1];
            if (ballToDelete != null)
            {
                elementBalls.RemoveAt(elementBalls.Count - 1);
                Destroy(ballToDelete.gameObject);
            }

        }
    }
}
