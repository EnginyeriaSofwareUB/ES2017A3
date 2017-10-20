using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemBase : ModuloTotem
{
    public ModuloTotemBase()
    {
        this.ataque = 10;
        this.defensa = 10;
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
