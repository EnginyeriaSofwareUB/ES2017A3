using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemGorila : ModuloTotem
{
    public ModuloTotemGorila()
    {
		this.ataque = 10;
		this.defensa = 0;
		this.vida = 0;
		this.movimiento = 0;
        this.getTipoTotem = TotemType.TOTEM_GORILA;

    }

    // Use this for initialization
    void Start () {
        this.gameObject.AddComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
