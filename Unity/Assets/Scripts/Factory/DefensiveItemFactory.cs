using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveItemFactory {

    DefensiveItem [] defensiveItems;

    public DefensiveItemFactory(int numItems)
    {
        defensiveItems = new DefensiveItem[numItems];
    }

    public DefensiveItem getItem()
    {
        bool trobat = false;
        DefensiveItem defensiveItem = null;
        int i = 0;
        while(!trobat && i < defensiveItems.Length)
        {
            defensiveItem = defensiveItems[i];
            if (!defensiveItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        return defensiveItem;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
