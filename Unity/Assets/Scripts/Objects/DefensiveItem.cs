using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum que contiene los tipos de objetos defensivos
/// </summary>
public enum DefensiveItemType
{
    Escut, EscutDoble, Iglú, ÀngelGuarda
}

public abstract class DefensiveItem : Item {

	protected int defense;
	protected int roundCounter;
	protected int maxRound;
	protected DefensiveItemType defensiveType;

	override protected void Start () {
		base.Start();
		this.roundCounter= 0;
		this.setDefense();
	}
	
	abstract protected void setDefense();

	override protected void setItemType(){
		this.type = ItemType.Defensa;
	}

	public int RoundCounter{
		get{
			return roundCounter;
		}
		set{
			roundCounter = value;
		}
	}

	public void IncreaseRound(){
		roundCounter += 1;	
	}

	protected bool MaxRound(){
		return this.roundCounter > this.maxRound;
	}

	protected void setItemUses(int uses){
		this.uses = uses;
	}

	public DefensiveItemType getDefensiveItemType(){
		if (defensiveType != null) {
			return defensiveType;
		}
		return null;

	}
		
	/// <summary>
	/// Al actualizar miramos si el objeto ha llegado a su máximo de rondas o a su máximo de usos
	/// </summary>
    override protected void Update()
    {
		if (this.MaxRound() || !this.isActive)
        {
            this.RemoveItem();
        }
    }
}
