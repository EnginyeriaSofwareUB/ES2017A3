using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackItemType
{
    Missil, GranadaFragmentacio, Semtex, BombaTradicional
}
public abstract class AttackItem : Item {

	protected CircleCollider2D destructionCircle;
    protected float damage;
	
	override protected void Start () {
		base.Start();
		this.setDamage();
		this.setDestructionCirlce();
	}

	abstract protected void setDamage();

	abstract protected void setDestructionCirlce();

	override protected void setItemType(){
		this.type = ItemType.Atac;
	}

}
