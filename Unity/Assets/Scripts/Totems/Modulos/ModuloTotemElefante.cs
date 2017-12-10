using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemElefante : ModuloTotem
{
	public ModuloTotemElefante()
	{
		this.ataque = 0;
		this.defensa = 0;
		this.vida = 5;
		this.movimiento = 0;
		this.getTipoTotem = TotemType.TOTEM_ELEFANTE;
		this.precio = 40;
	}


    // Update is called once per frame
    void Update () {

	}
}
