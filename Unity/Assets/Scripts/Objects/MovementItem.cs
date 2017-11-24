using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementItemType
{
    Coet, BolaTeletransport,Raig
}

public abstract class MovementItem : Item {

	override protected void setItemType(){
		this.type = ItemType.Moviment;
	}
}
