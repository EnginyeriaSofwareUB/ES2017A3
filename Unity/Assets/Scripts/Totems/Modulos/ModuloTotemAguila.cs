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




    }

    // Use this for initialization
    void Start () {
        this.gameObject.AddComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
