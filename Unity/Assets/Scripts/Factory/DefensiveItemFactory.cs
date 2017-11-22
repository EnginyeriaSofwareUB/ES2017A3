using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefensiveItemFactory {

	Dictionary <DefensiveItemType,DefensiveItem> defensiveItems;
	private int numCharacters;

    public DefensiveItemFactory(int numCharacters)
    {
		this.numCharacters = numCharacters;
		CreateDefensiveItems ();
    }

	public void CreateDefensiveItems(){
		Array defensiveTypeItems = Enum.GetValues (typeof(DefensiveItemType));
		defensiveItems = new Dictionary<DefensiveItemType,DefensiveItem> ();

		foreach (DefensiveItemType dType in defensiveTypeItems) {
			for (int i = 0; i < numCharacters; i++) {
				switch (dType) {
					case DefensiveItemType.Escut:
						defensiveItems.Add(dType, new Escut (dType));
						break;
					case DefensiveItemType.EscutDoble:
						defensiveItems.Add(dType, new EscutDoble (dType));
						break;
					case DefensiveItemType.Iglú:
						defensiveItems.Add(dType, new Iglu (dType));
						break;
					case DefensiveItemType.ÀngelGuarda:
						defensiveItems.Add(dType, new AngelGuarda (dType));
						break;
				}
			}
		}
	}

	public DefensiveItem GetIglu(){
		return GetItem (DefensiveItemType.Iglú);
	}

	public DefensiveItem GetEscut(){
		return GetItem (DefensiveItemType.Escut);
	}

	public DefensiveItem GetEscutDoble(){
		return GetItem (DefensiveItemType.EscutDoble);
	}

	public DefensiveItem GetAngelGuarda(){
		return GetItem (DefensiveItemType.ÀngelGuarda);
	}

	public DefensiveItem GetItem(DefensiveItemType defensiveItemType)
    {
        bool trobat = false;
        DefensiveItem defensiveItem = null;

        int i = 0;
		while(!trobat && i < defensiveItems[DefensiveItemType].Count)
        {
			defensiveItem = defensiveItems[DefensiveItemType][i];
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
}
