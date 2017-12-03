using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotemType
{
    TOTEM_GORILA,
    TOTEM_BASE,
    TOTEM_AGUILA,
	TOTEM_TORTUGA,
	TOTEM_ELEFANTE
};

public class ModuloTotem : MonoBehaviour {

    protected int ataque;
    protected int defensa;
	protected int movimiento; 
	protected int vida;
    protected TotemType getTipoTotem;
    //private Transform meshTotem;
    public TotemType getTypeOfTotem()
    {
        return getTipoTotem;
    }

   

    // Use this for initialization
    void Start () {
        this.gameObject.AddComponent<CircleCollider2D>();

        inicializarContorno();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected void inicializarContorno()
    {
        transform.GetChild(0).gameObject.AddComponent<cakeslice.Outline>();
        resetColorContorno();
    }
    /// <summary>
    /// Permite inicializar el contorno del módulo dependiendo si el totem es del primer jugador o del segundo jugador.
    /// </summary>
    public void resetColorContorno()
    {
        if (this.gameObject.layer == Assets.Scripts.Environment.Global.Capas.totemsPrimerJugador)
            transform.GetChild(0).GetComponent<cakeslice.Outline>().color = 0;
        else
            transform.GetChild(0).GetComponent<cakeslice.Outline>().color = 1;
    }


    public int getAtaque()
    {
        return ataque;
    }

    public int getDefensa()
    {
        return defensa;
    }

	public int getMovimiento()
	{
		return movimiento;
	}

	public int getVida()
	{
		return vida;
	}

}
