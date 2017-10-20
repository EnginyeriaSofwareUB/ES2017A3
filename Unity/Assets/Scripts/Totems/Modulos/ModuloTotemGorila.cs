using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemGorila : ModuloTotem
{
    public ModuloTotemGorila()
    {
        this.ataque = 20;
        this.defensa = 10;
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
