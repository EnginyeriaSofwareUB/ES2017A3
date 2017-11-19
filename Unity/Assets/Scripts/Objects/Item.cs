using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Atac, Curació, Defensa, Moviment
}
abstract public class Item : MonoBehaviour {

    protected bool isActive;
	protected bool isUsed;
	protected bool canBeEquipedWithOthers;

	protected ItemType type;

	private Totem holder;

    protected int uses;
    protected int useCounter;
    private bool isTaken;

    // Use this for initialization
    virtual protected void Start () {
		this.isActive = false;
		this.isUsed = false;
		this.holder = null;
		this.useCounter = 0;
        this.isTaken = false;
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

	protected void RemoveItem(){
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

    public bool IsTaken()
    {
        return isTaken;
    }

    public void SetTaken(bool isTaken)
    {
        this.isTaken = isTaken;
    }

}
