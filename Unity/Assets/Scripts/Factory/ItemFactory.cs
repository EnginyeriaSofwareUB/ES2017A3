using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory {

    HealingItemFactory healingFactory;
    DefensiveItemFactory defensiveFactory;
    AttackItemFactory attackFactory;
    MovementItemFactory movementFactory;

	private int numCharacters;

    public ItemFactory(int numCharacters)
    {
		this.numCharacters = numCharacters;
        InitDefensiveItemFactory();
        InitAttackItemFactory();
        InitHealingItemFactory();
        InitMovementItemFactory();
    }

    public HealingItem getHealingItem()
    {
        return healingFactory.getItem();
    }

    public AttackItem getAttackItem()
    {
        return attackFactory.getItem();
    }

    public MovementItem getMovementItem()
    {
        return movementFactory.getItem();
    }

    public DefensiveItem getDefensiveItem()
    {
        return defensiveFactory.getItem();
    }

    // Use this for initialization
    public void InitDefensiveItemFactory()
    {
        InitItemFactory(ItemType.Defensa);
    }

    public void InitAttackItemFactory()
    {
        InitItemFactory(ItemType.Atac);
    }

    public void InitHealingItemFactory()
    {
        InitItemFactory(ItemType.Curació);
    }

    public void InitMovementItemFactory()
    {
        InitItemFactory(ItemType.Moviment);
    }


    private void InitItemFactory(ItemType item)
    {
        switch (item)
        {
            case ItemType.Curació:
			healingFactory = new HealingItemFactory(numCharacters);
                break;
            case ItemType.Atac:
			attackFactory = new AttackItemFactory(numCharacters);
                break;
            case ItemType.Defensa:
			defensiveFactory = new DefensiveItemFactory(numCharacters);
                break;
            case ItemType.Moviment:
			movementFactory = new MovementItemFactory(numCharacters);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
