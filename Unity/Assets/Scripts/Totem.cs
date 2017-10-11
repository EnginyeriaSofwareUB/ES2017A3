using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Totem : MonoBehaviour
{

    private int ataqueTotal { get; set; }
    private int defensaTotal { get; set; }

    private List <ModuloTotem> modulos;

    protected GameObject gameObjectTotem { get; set; }

    public Totem(int ataque, int defensa)
    {
        this.ataqueTotal = ataque;
        this.defensaTotal = defensa;
    }

    public Totem()
    {
        modulos = new List<ModuloTotem>();
    }

    public void CreateTotem()
    {
        AddModule(TotemType.TOTEM_BASE);
    }

    public void AddGorilaTotem()
    {
        AddModule(TotemType.TOTEM_GORILA);
    }

    public void AddAguilaTotem()
    {
        AddModule(TotemType.TOTEM_AGUILA);
    }

    public void DeleteGorilaTotem()
    {
        DeleteModule(TotemType.TOTEM_GORILA);
    }

    public void DeleteAguilaTotem()
    {
        DeleteModule(TotemType.TOTEM_AGUILA);

    }

    private void DeleteModule(TotemType totem)
    {
  
        int position = searchModule(totem);
        try
        {
            modulos.RemoveAt(position);
        }
        catch (TotemException tE)
        {
            Console.WriteLine("Totem exception: " + tE.Message);
        }
    }

    private void AddModule(TotemType totem)
    {
        switch (totem)
        {
            case TotemType.TOTEM_BASE:
                modulos.Add(new ModuloTotemBase());
                break;
            case TotemType.TOTEM_AGUILA:
                modulos.Add(new ModuloTotemAguila());
                break;
            case TotemType.TOTEM_GORILA:
                modulos.Add(new ModuloTotemGorila());
                break;
        }
        ModuloTotem lastModuleAdded = modulos[modulos.Count-1];
        this.ataqueTotal += lastModuleAdded.getAtaque(); //Sumamos el ataque que nos ha aportado el modulo añadido al totem
        this.defensaTotal += lastModuleAdded.getDefensa(); //Sumamos la defensa del modulo añadido al totem
    }

    private int searchModule(TotemType type)
    {
        int position =0;
        bool trobat = false;
        while (position < modulos.Count && !trobat)
        {
            if (modulos[position].getTypeOfTotem() == type)
            {
                trobat = true;
            }
            else
            {
                position += 1;
            }       
        }

        if (trobat)
        {
            return position;
        }
        throw new TotemException("Module not found");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
