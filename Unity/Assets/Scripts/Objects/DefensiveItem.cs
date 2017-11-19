using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefensiveItemType
{
    Escut, EscutDoble, Iglú, ÀngelGuarda
}

public abstract class DefensiveItem : Item {

	protected int defense;
	override protected void Start () {
		base.Start();
		this.setDefense();
	}
	
	abstract protected void setDefense();

	override protected void setItemType(){
		this.type = ItemType.Defensa;
	}

    override protected void Update()
    {
        if (this.isUsed || !this.isActive)
        {
            this.RemoveItem();
        }
    }
}
