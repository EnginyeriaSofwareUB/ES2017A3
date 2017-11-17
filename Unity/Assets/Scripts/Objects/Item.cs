using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour {

	public enum ItemType {
		Atac, Curació, Defensa, Moviment
	}

	private bool isActive;
	private bool isUsed;
	protected bool canBeEquipedWithOthers;

	protected ItemType type;

	private Totem holder;

	protected int uses;
	private int useCounter;

	// Use this for initialization
	virtual protected void Start () {
		this.isActive = false;
		this.isUsed = false;
		this.holder = null;
		this.useCounter = 0;
		this.setCanBeEquipedWithOthers();
		this.setItemUses();
		this.setItemType();
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		if(isUsed && !isActive){
			this.RemoveItem();
		}
	}

	private void RemoveItem(){
		Destroy(this);
	}

	abstract public void applyEffect();

	abstract public void removeEffect();

	abstract public ItemType GetItemType();

	virtual protected void setCanBeEquipedWithOthers(){
		this.canBeEquipedWithOthers = false;
	}

	abstract protected void setItemUses();

	abstract protected void setItemType();

	public void UseItem(){
		if(!this.isUsed){
			this.isUsed = true;
		}
		this.useCounter ++;
		this.isActive = this.useCounter < this.uses;
		this.applyEffect();
	}

	public bool isItemActive(){
		return this.isUsed && this.isActive; 
	}

	public bool isEquipableWithOthers(){
		return this.canBeEquipedWithOthers;
	}

	public bool giveItem(Totem holder) {
		if(this.holder == null){
			this.holder = holder;
			return true;
		} else {
			return false;
		}
	}
}
