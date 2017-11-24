using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EscutDoble : DefensiveItem {

	public EscutDoble(){
        this.defensiveType = (DefensiveItemType)Enum.Parse(typeof(DefensiveItemType), this.name, true);
    }

    public override void applyEffect()
    {
        throw new System.NotImplementedException();
    }

    public override ItemType GetItemType()
    {
        throw new System.NotImplementedException();
    }

    public override void removeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void setDefense()
    {
        throw new System.NotImplementedException();
    }

    protected override void setItemUses()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SecondPlayer" || collision.gameObject.tag == "FirstPlayer")
        {
            giveItem(collision.gameObject.GetComponent<Totem>());
        }
    }
}
