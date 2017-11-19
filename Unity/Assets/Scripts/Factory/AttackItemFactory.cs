using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemFactory  {

    AttackItem [] attackItems;

    public AttackItemFactory(int numItems)
    {
        attackItems = new AttackItem[numItems];
    }

    public AttackItem getItem()
    {
        bool trobat = false;
        AttackItem attackItem = null;
        int i = 0;
        while (!trobat && i < attackItems.Length)
        {
            attackItem = attackItems[i];
            if (!attackItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        attackItem.SetTaken(true);
        return attackItem;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
