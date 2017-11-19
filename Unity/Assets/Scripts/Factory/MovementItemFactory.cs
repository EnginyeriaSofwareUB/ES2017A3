using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementItemFactory {

    MovementItem [] movementItems;

    public MovementItemFactory(int numItems)
    {
        movementItems = new MovementItem[numItems];
    }

    public MovementItem getItem()
    {
        bool trobat = false;
        MovementItem movementItem = null;
        int i = 0;
        while (!trobat && i < movementItems.Length)
        {
            movementItem = movementItems[i];
            if (!movementItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        movementItem.SetTaken(true);
        return movementItem;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
