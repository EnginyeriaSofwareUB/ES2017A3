using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefensiveItemFactory {

	Dictionary <DefensiveItemType, ArrayList> defensiveItems;
	private int numCharacters;

    public DefensiveItemFactory(int numCharacters)
    {
		this.numCharacters = numCharacters;
		CreateDefensiveItems ();
    }

	public void CreateDefensiveItems(){
		Array defensiveTypeItems = Enum.GetValues (typeof(DefensiveItemType));
		defensiveItems = new Dictionary<DefensiveItemType,ArrayList> ();
        ArrayList escuts = new ArrayList();
        ArrayList escutsDobles = new ArrayList();
        ArrayList iglus = new ArrayList();
        ArrayList angelsGuarda = new ArrayList();

        foreach (DefensiveItemType dType in defensiveTypeItems) {
			for (int i = 0; i < numCharacters; i++) {
				switch (dType) {
					case DefensiveItemType.Escut:
                        escuts.Add(new Escut());
						break;
					case DefensiveItemType.EscutDoble:
                        escutsDobles.Add(new EscutDoble());
                        break;
					case DefensiveItemType.Iglu:
                        iglus.Add(new Iglu());
                        break;
					case DefensiveItemType.AngelGuarda:
                        angelsGuarda.Add(new AngelGuarda());
                        break;
				}
			}
		}
        defensiveItems.Add(DefensiveItemType.Escut, escuts);
        defensiveItems.Add(DefensiveItemType.EscutDoble, escutsDobles);
        defensiveItems.Add(DefensiveItemType.Iglu, iglus);
        defensiveItems.Add(DefensiveItemType.AngelGuarda, angelsGuarda);

    }

	public DefensiveItem GetIglu(){
		return GetItem (DefensiveItemType.Iglu);
	}

	public DefensiveItem GetEscut(){
		return GetItem (DefensiveItemType.Escut);
	}

	public DefensiveItem GetEscutDoble(){
		return GetItem (DefensiveItemType.EscutDoble);
	}

	public DefensiveItem GetAngelGuarda(){
		return GetItem (DefensiveItemType.AngelGuarda);
	}

    public DefensiveItem GetItem(DefensiveItemType defensiveItemType)
    {
        bool trobat = false;
        DefensiveItem defensiveItem = null;

        int i = 0;
        while (!trobat && i < defensiveItems[defensiveItemType].Count)
        {
            defensiveItem = (DefensiveItem)defensiveItems[defensiveItemType][i];
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
