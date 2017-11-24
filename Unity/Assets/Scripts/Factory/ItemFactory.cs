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

    /// <summary>
    /// Defensive items
    /// </summary>
    /// <returns></returns>

    public DefensiveItem GetIglu()
    {
        return defensiveFactory.GetIglu();
    }

    public DefensiveItem GetEscut()
    {
        return defensiveFactory.GetEscut();
    }

    public DefensiveItem GetEscutDoble()
    {
        return defensiveFactory.GetEscutDoble();
    }

    public DefensiveItem GetAngelGuarda()
    {
        return defensiveFactory.GetAngelGuarda();
    }

    /// <summary>
    /// Attack items
    /// </summary>
    /// <returns></returns>

    public AttackItem GetMissil()
    {
        return attackFactory.GetMissil();
    }

    public AttackItem GetGranadaFragmentacio()
    {
        return attackFactory.GetGranadaFragmentacio();
    }

    public AttackItem GetSemtex()
    {
        return attackFactory.GetSemtex();
    }

    public AttackItem GetBombaTradicional()
    {
        return attackFactory.GetBombaTradicional();
    }

    /// <summary>
    /// Healing items
    /// </summary>
    /// <returns></returns>

    public HealingItem GetBotiqui()
    {
        return healingFactory.GetBotiqui();
    }

    public HealingItem GetVitamina()
    {
        return healingFactory.GetVitamina();
    }

    /// <summary>
    /// Movement items
    /// </summary>
    /// <returns></returns>
    
    public MovementItem GetBolaTeletransport()
    {
        return movementFactory.GetBolaTeletransport();
    }

    public MovementItem GetCoet()
    {
        return movementFactory.GetCoet();
    }

    public MovementItem GetRaig()
    {
        return movementFactory.GetRaig();
    }

    /// <summary>
    /// Initialising factories
    /// </summary>
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

    /// <summary>
    /// Create a factory
    /// </summary>
    /// <param name="item"></param>
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
    
}
