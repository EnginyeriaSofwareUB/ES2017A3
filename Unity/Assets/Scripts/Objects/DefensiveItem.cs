using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
