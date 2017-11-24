using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealingItemType
{
    Vitamina,Botiqui
}

public abstract class HealingItem : Item {

	protected int heal;

	override protected void Start(){
		base.Start();
		this.setHealingPower();
	}

	override protected void setItemType(){
		this.type = ItemType.Curació;
	}

	abstract protected void setHealingPower();
}
