using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemAguila : ModuloTotem
{
    public ModuloTotemAguila()
    {
		this.ataque = 0;
		this.defensa = 0;
		this.vida = 0;
		this.movimiento = 10;
        this.getTipoTotem = TotemType.TOTEM_AGUILA;
		this.precio = 40;

    }

   
	
	// Update is called once per frame
	void Update () {
		
	}
}
