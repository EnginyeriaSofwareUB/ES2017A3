using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemBase : ModuloTotem
{
    public ModuloTotemBase()
    {
        this.ataque = 5;
        this.defensa = 5;
		this.vida = 5;
		this.movimiento = 5;
        this.getTipoTotem = TotemType.TOTEM_BASE;

    }

    public void Awake()
    {
    
    }

    // Use this for initialization
    void Start()
    {
        this.gameObject.AddComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
