using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealingItemFactory {

    Dictionary<HealingItemType, ArrayList> healingItems;
    private int numCharacters;

    public HealingItemFactory(int numCharacters)
    {
        this.numCharacters = numCharacters;
        CreateHealingItems();
    }

    public void CreateHealingItems()
    {
        Array healingTypeItems = Enum.GetValues(typeof(HealingItemType));
        healingItems = new Dictionary<HealingItemType, ArrayList>();
        ArrayList botiquins = new ArrayList();
        ArrayList vitamines = new ArrayList();

        foreach (HealingItemType dType in healingTypeItems)
        {
            for (int i = 0; i < numCharacters; i++)
            {
                switch (dType)
                {
                    case HealingItemType.Botiqui:
                        //botiquins.Add(new Botiqui());
                        break;
                    case HealingItemType.Vitamina:
                        //vitamines.Add(new Vitamina());
                        break;
                }
            }
        }
        healingItems.Add(HealingItemType.Botiqui, botiquins);
        healingItems.Add(HealingItemType.Vitamina, vitamines);

    }

    public HealingItem GetBotiqui()
    {
        return GetItem(HealingItemType.Botiqui);
    }

    public HealingItem GetVitamina()
    {
        return GetItem(HealingItemType.Vitamina);
    }

    public HealingItem GetItem(HealingItemType HealingItemType)
    {
        bool trobat = false;
        HealingItem healingItem = null;

        int i = 0;
        while (!trobat && i < healingItems[HealingItemType].Count)
        {
            healingItem = (HealingItem)healingItems[HealingItemType][i];
            if (!healingItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        return healingItem;
    }
}
