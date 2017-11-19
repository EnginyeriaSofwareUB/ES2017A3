using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItemFactory {

    HealingItem [] healingItems;

    public HealingItemFactory(int numItems)
    {
        healingItems = new HealingItem[numItems];
    }

    public HealingItem getItem()
    {
        bool trobat = false;
        HealingItem healingItem = null;
        int i = 0;
        while (!trobat && i < healingItems.Length)
        {
            healingItem = healingItems[i];
            if (!healingItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        healingItem.SetTaken(true);
        return healingItem;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
