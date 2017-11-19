using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory {

    HealingItemFactory healingFactory;
    DefensiveItemFactory defensiveFactory;
    AttackItemFactory attackFactory;
    MovementItemFactory movementFactory;

    public ItemFactory(int numItems)
    {
        InitDefensiveItemFactory(numItems);
        InitAttackItemFactory(numItems);
        InitHealingItemFactory(numItems);
        InitMovementItemFactory(numItems);
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
    public void InitDefensiveItemFactory(int numItems)
    {
        InitItemFactory(ItemType.Defensa, numItems);
    }

    public void InitAttackItemFactory(int numItems)
    {
        InitItemFactory(ItemType.Atac, numItems);
    }

    public void InitHealingItemFactory(int numItems)
    {
        InitItemFactory(ItemType.Curació, numItems);
    }

    public void InitMovementItemFactory(int numItems)
    {
        InitItemFactory(ItemType.Moviment, numItems);
    }


    private void InitItemFactory(ItemType item, int numItems)
    {
        switch (item)
        {
            case ItemType.Curació:
                healingFactory = new HealingItemFactory(numItems);
                break;
            case ItemType.Atac:
                attackFactory = new AttackItemFactory(numItems);
                break;
            case ItemType.Defensa:
                defensiveFactory = new DefensiveItemFactory(numItems);
                break;
            case ItemType.Moviment:
                movementFactory = new MovementItemFactory(numItems);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
