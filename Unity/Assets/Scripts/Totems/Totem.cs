﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{

    [SerializeField]  private int ataqueTotal { get; set; }
    [SerializeField]  private int defensaTotal { get; set; }
    [SerializeField] private List <ModuloTotem> modulos;

    //Manejador del movimiento del jugador
    private MovimientoController movimiento;

    public Totem(int ataque, int defensa)
    {
        this.ataqueTotal = ataque;
        this.defensaTotal = defensa;

    }

 

    public Totem()
    {
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

        ModuloTotem lastModuleAdded = modulos[modulos.Count - 1];

        // En caso que sea el primer módulo que se añade
        if (modulos.Count < 2)
        {
            lastModuleAdded.MeshTotem.position = this.transform.position;
            // Subimos la posición del totem para apilarlo
            lastModuleAdded.MeshTotem.transform.parent = this.transform;
        }
        else
        {
            ModuloTotem moduloAnterior = modulos[modulos.Count - 2];

            lastModuleAdded.MeshTotem.position = moduloAnterior.MeshTotem.transform.position;
            // Subimos la posición del totem para apilarlo
           lastModuleAdded.MeshTotem.position = lastModuleAdded.MeshTotem.position+ moduloAnterior.MeshTotem.transform.up*0.7f;
           lastModuleAdded.MeshTotem.transform.parent = this.transform;
        }




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
        AddModule(TotemType.TOTEM_BASE);
        AddModule(TotemType.TOTEM_AGUILA);
       AddModule(TotemType.TOTEM_GORILA);
        this.movimiento = GetComponent<MovimientoController>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void desabilitarControlMovimiento()
    {
        this.movimiento.PuedeMoverse = false;
    }


    public void activarControlMovimiento()
    {
        this.movimiento.PuedeMoverse = true;
    }
    public bool excedeLimiteDistancia()
    {
        return this.movimiento.isLimitePasos();
    }
}
