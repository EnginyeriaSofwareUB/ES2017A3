using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackItemFactory  {

    Dictionary<AttackItemType, ArrayList> attackItems;
    private int numCharacters;

    public AttackItemFactory(int numCharacters)
    {
        this.numCharacters = numCharacters;
        CreateAttackItems();
    }

    public void CreateAttackItems()
    {
        Array attackTypeItems = Enum.GetValues(typeof(AttackItemType));
        attackItems = new Dictionary<AttackItemType, ArrayList>();
        ArrayList missils = new ArrayList();
        ArrayList granadesFrag = new ArrayList();
        ArrayList semtexs = new ArrayList();
        ArrayList bombesTradicionals = new ArrayList();

        foreach (AttackItemType dType in attackTypeItems)
        {
            for (int i = 0; i < numCharacters; i++)
            {
                switch (dType)
                {
                    case AttackItemType.Missil:
                        //missils.Add(new Missil());
                        break;
                    case AttackItemType.GranadaFragmentacio:
                        //granadesFrag.Add(new GranadaFragmentacio());
                        break;
                    case AttackItemType.Semtex:
                        //semtexs.Add(new Semtex());
                        break;
                    case AttackItemType.BombaTradicional:
                        //bombesTradicionals.Add(new BombaTradicional());
                        break;
                }
            }
        }
        attackItems.Add(AttackItemType.Missil, missils);
        attackItems.Add(AttackItemType.GranadaFragmentacio, granadesFrag);
        attackItems.Add(AttackItemType.Semtex, semtexs);
        attackItems.Add(AttackItemType.BombaTradicional, bombesTradicionals);

    }

    public AttackItem GetMissil()
    {
        return GetItem(AttackItemType.Missil);
    }

    public AttackItem GetGranadaFragmentacio()
    {
        return GetItem(AttackItemType.GranadaFragmentacio);
    }

    public AttackItem GetSemtex()
    {
        return GetItem(AttackItemType.Semtex);
    }

    public AttackItem GetBombaTradicional()
    {
        return GetItem(AttackItemType.BombaTradicional);
    }

    public AttackItem GetItem(AttackItemType attackItemType)
    {
        bool trobat = false;
        AttackItem attackItem = null;

        int i = 0;
        while (!trobat && i < attackItems[attackItemType].Count)
        {
            attackItem = (AttackItem)attackItems[attackItemType][i];
            if (!attackItem.IsTaken())
            {
                trobat = true;
            }
            else
            {
                i += 1;
            }

        }
        return attackItem;
    }
}
