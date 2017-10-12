using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemAguila : ModuloTotem
{
    public ModuloTotemAguila()
    {
        this.ataque = 10;
        this.defensa = 20;
        this.getTipoTotem = TotemType.TOTEM_AGUILA;

        GameObject instance = Instantiate(Resources.Load("TotemFalcon", typeof(GameObject))) as GameObject;
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