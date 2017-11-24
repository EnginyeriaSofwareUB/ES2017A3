using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementItemFactory {

    Dictionary<MovementItemType, ArrayList> movementItems;
    private int numCharacters;

    public MovementItemFactory(int numCharacters)
    {
        this.numCharacters = numCharacters;
        CreateMovementItems();
    }

    public void CreateMovementItems()
    {
        Array healingTypeItems = Enum.GetValues(typeof(MovementItemType));
        movementItems = new Dictionary<MovementItemType, ArrayList>();
        ArrayList bolesTeletransport = new ArrayList();
        ArrayList coets = new ArrayList();
        ArrayList raigs = new ArrayList();

        foreach (MovementItemType dType in healingTypeItems)
        {
            for (int i = 0; i < numCharacters; i++)
            {
                switch (dType)
                {
                    case MovementItemType.BolaTeletransport:
                        //bolesTeletransport.Add(new Missil());
                        break;
                    case MovementItemType.Coet:
                        //coets.Add(new GranadaFragmentacio());
                        break;
                    case MovementItemType.Raig:
                        //raigs.Add(new Semtex());
                        break;
                }
            }
        }
        movementItems.Add(MovementItemType.BolaTeletransport, bolesTeletransport);
        movementItems.Add(MovementItemType.Coet, coets);
        movementItems.Add(MovementItemType.Raig, raigs);

    }

    public MovementItem GetBolaTeletransport()
    {
        return GetItem(MovementItemType.BolaTeletransport);
    }

    public MovementItem GetCoet()
    {
        return GetItem(MovementItemType.Coet);
    }

    public MovementItem GetRaig()
    {
        return GetItem(MovementItemType.Raig);
    }

    public MovementItem GetItem(MovementItemType MovementItemType)
    {
        bool trobat = false;
        MovementItem movementItem = null;

        int i = 0;
        while (!trobat && i < movementItems[MovementItemType].Count)
        {
            movementItem = (MovementItem)movementItems[MovementItemType][i];
            if (!movementItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        return movementItem;
    }
}
