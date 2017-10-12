using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotemType
{
    TOTEM_GORILA,
    TOTEM_BASE,
    TOTEM_AGUILA
};

public class ModuloTotem : MonoBehaviour {

    protected int ataque;
    protected int defensa;
    protected TotemType getTipoTotem;
    private Transform meshTotem;
    public TotemType getTypeOfTotem()
    {
        return getTipoTotem;
    }

   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getAtaque()
    {
        return ataque;
    }

    public int getDefensa()
    {
        return defensa;
    }

    public Transform MeshTotem
    {
        get { return this.meshTotem; }
        set { this.meshTotem = value; }
    }
}
