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

        GameObject instance = Instantiate(Resources.Load("TotemGorilla", typeof(GameObject))) as GameObject;
        this.MeshTotem = instance.transform;

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