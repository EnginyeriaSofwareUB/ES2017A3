﻿using Assets.Scripts.Environment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YounGenTech.HealthScript;

public class Totem : MonoBehaviour
{

    [SerializeField] private int ataqueTotal { get; set; }
    [SerializeField] private int defensaTotal { get; set; }
	[SerializeField] private int movimientoTotal { get; set; }
	[SerializeField] private int vidaTotal { get; set; }
    [SerializeField] private float maxHealth=100f;
    [SerializeField] public float currentHealth;

    [SerializeField] private List<GameObject> modulos;

    [SerializeField] public List<int> totemItems;

    //Manejador del movimiento del jugador
    private MovimientoController movimiento;

    private GameObject gameManager;

    public GameObject deathExplosion, fallExplosion, onHitEffect;

	public Totem(int ataque, int defensa, int movimiento, int vida)
    {
        this.ataqueTotal = ataque;
        this.defensaTotal = defensa;
		this.movimientoTotal = movimiento;
		this.vidaTotal = vida;
    }



    public Totem()
    {
    }

    public void CreateTotem()
    {
        AddModule(TotemType.TOTEM_BASE);
    }

    public void AddGorilaTotem()
    {
        AddModule(TotemType.TOTEM_GORILA);
    }

    public void AddAguilaTotem()
    {
        AddModule(TotemType.TOTEM_AGUILA);
    }

	public void AddTortugaTotem()
	{
		AddModule(TotemType.TOTEM_TORTUGA);
	}

	public void AddElefanteTotem()
	{
		AddModule(TotemType.TOTEM_ELEFANTE);
	}

    public void DeleteGorilaTotem()
    {
        DeleteModule(TotemType.TOTEM_GORILA);
    }

    public void DeleteAguilaTotem()
    {
        DeleteModule(TotemType.TOTEM_AGUILA);

    }

    private void DeleteModule(TotemType totem)
    {
  
        int position = searchModule(totem);
        try
        {
            modulos.RemoveAt(position);
        }
        catch (TotemException tE)
        {
            Console.WriteLine("Totem exception: " + tE.Message);
        }
    }

    private void AddModule(TotemType totem)
    {
        GameObject instance;
        switch (totem)
        {
            case TotemType.TOTEM_BASE:
                instance = Instantiate(Resources.Load("TotemBase", typeof(GameObject))) as GameObject;
                instance.AddComponent<ModuloTotemBase>();
                // Agregamos la misma layer y así evitamos las colisiones
                instance.layer = GetComponent<Totem>().gameObject.layer;
                instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
                modulos.Add(instance);
                break;
            case TotemType.TOTEM_AGUILA:
                instance = Instantiate(Resources.Load("TotemFalcon", typeof(GameObject))) as GameObject;
                instance.AddComponent<ModuloTotemAguila>();
                instance.layer = GetComponent<Totem>().gameObject.layer;
                instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
                modulos.Add(instance);
                break;
            case TotemType.TOTEM_GORILA:
                instance = Instantiate(Resources.Load("TotemGorilla", typeof(GameObject))) as GameObject;
                instance.AddComponent<ModuloTotemGorila>();
                instance.layer = GetComponent<Totem>().gameObject.layer;
                instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
                modulos.Add(instance);
                break;
			case TotemType.TOTEM_TORTUGA:
				instance = Instantiate(Resources.Load("TotemTurtle", typeof(GameObject))) as GameObject;
				instance.AddComponent<ModuloTotemTortuga>();
				instance.layer = GetComponent<Totem>().gameObject.layer;
				instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
				modulos.Add(instance);
				break;
			case TotemType.TOTEM_ELEFANTE:
				instance = Instantiate(Resources.Load("TotemElephant", typeof(GameObject))) as GameObject;
				instance.AddComponent<ModuloTotemElefante>();
				instance.layer = GetComponent<Totem>().gameObject.layer;
				instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
				modulos.Add(instance);
				break;
        }

        GameObject lastModuleAdded = modulos[modulos.Count - 1];

        // Sumamos los parámetros de los módulos al totem
        this.ataqueTotal += lastModuleAdded.GetComponent<ModuloTotem>().getAtaque();
        this.defensaTotal += lastModuleAdded.GetComponent<ModuloTotem>().getDefensa();
		this.movimientoTotal += lastModuleAdded.GetComponent<ModuloTotem>().getMovimiento();
		this.vidaTotal += lastModuleAdded.GetComponent<ModuloTotem>().getVida();



        // En caso que sea el primer módulo que se añade
        if (modulos.Count < 2)
        {
            lastModuleAdded.transform.position = this.transform.position;
            // Subimos la posición del totem para apilarlo
            lastModuleAdded.transform.parent = this.transform;

        }
        else
        {
            GameObject moduloAnterior = modulos[modulos.Count - 2];

            lastModuleAdded.transform.position = moduloAnterior.transform.position;
            // Subimos la posición del totem para apilarlo
            lastModuleAdded.transform.position = lastModuleAdded.transform.position + moduloAnterior.transform.up * 0.7f;
            lastModuleAdded.transform.parent = this.transform;
        }
        this.movimiento.DistanciaLimite += movimientoTotal;
		aumentarVida(vidaTotal);


    }

    private int searchModule(TotemType type)
    {
        int position = 0;
        bool trobat = false;
        while (position < modulos.Count && !trobat)
        {
            if (modulos[position].GetComponent<ModuloTotem>().getTypeOfTotem() == type)
            {
                trobat = true;
            }
            else
            {
                position += 1;
            }
        }

        if (trobat)
        {
            return position;
        }
        throw new TotemException("Module not found");
    }

    // Use this for initialization
    void Awake()
    {
		this.movimiento = GetComponent<MovimientoController>();
        this.gameManager = GameObject.FindGameObjectWithTag("GameController");
        this.currentHealth = this.maxHealth;
        this.totemItems = new List<int>();
        /*AddItem(Global.TIPO_OBJETOS.objetoAngel);
        AddItem(Global.TIPO_OBJETOS.objetoEscudoSimple);
        AddItem(Global.TIPO_OBJETOS.objetoEscudoSimple);*/

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
		kill ();
    }

    private void deleteLineRenderer()
    {
        //GameObject drawnLine = transform.Find("Arrow").gameObject;
        //Destroy(drawnLine);
    }

	private void kill(){
		if (this.currentHealth < 1)
		{
            if (AngelGuardaActivado())
            {
                StartCoroutine(RevivirTotem());
            }
            else
            {
                this.eliminarTotem();
            }
            
        }
	}



    public void eliminarTotem()
    {
            if (this.currentHealth > 0){
                Vector3 updatedPosition = this.gameObject.transform.position;
                if (updatedPosition.x < -20) updatedPosition.x = -20;
                else if (updatedPosition.x > 37) updatedPosition.x = 37;
                updatedPosition.y = -20;
                GameObject executeFallExplosion = Instantiate(this.fallExplosion,updatedPosition,this.gameObject.transform.rotation);
                executeFallExplosion.tag = "KillMe";
                Destroy(executeFallExplosion,executeFallExplosion.GetComponent<AudioSource>().clip.length);
            }else{
                GameObject executeDeathExplosion = Instantiate(this.deathExplosion,this.gameObject.transform.position,this.deathExplosion.transform.rotation);
                executeDeathExplosion.tag = "KillMe";
                Destroy(executeDeathExplosion, executeDeathExplosion.GetComponent<AudioSource>().clip.length);
            }
            if (AngelGuardaActivado())
            {
                StartCoroutine(RevivirTotem());
            }
            else
            {
                gameManager.SendMessage("RemoveTotem", this);
                this.movimiento.endMovement();
                this.currentHealth = 0;
                deleteLineRenderer();
            }
    }

    public void suicide()
    {
        if (!AngelGuardaActivado())
        {
            this.eliminarTotem();
        }
        else
        {
            StartCoroutine(RevivirTotem());
        }

    }

    public void desabilitarControlMovimiento()
    {
        this.movimiento.PuedeMoverse = false;
    }


    public void activarControlMovimiento()
    {
        this.movimiento.PuedeMoverse = true;
    }
    public bool excedeLimiteDistancia()
    {
        bool t = this.movimiento.isLimitePasos();
        if (t)
            desabilitarControlMovimiento();
        return this.movimiento.isShoot();
    }

    public string distanciaRestante()
    {
        return System.Math.Round(this.movimiento.GetDistanciaRecorrida(),1).ToString() + "/" + this.movimiento.DistanciaLimite.ToString();
    }

    public List<GameObject> Modulos
    {
        get
        {
            return modulos;
        }

        set
        {
            modulos = value;
        }
    }

    public void DecreaseVida()
    {
        Vector3 updatedPosition = this.gameObject.transform.position;
        updatedPosition.z = 0.5f;
        GameObject executeOnHitEffect = Instantiate(this.onHitEffect,updatedPosition,this.onHitEffect.transform.rotation);
        executeOnHitEffect.GetComponent<AudioSource>().Play();
        Destroy(executeOnHitEffect, executeOnHitEffect.GetComponent<AudioSource>().clip.length);
    }

	public void aumentarVida(float cantidad)
	{
		this.currentHealth += cantidad;
        SendMessage("Heal", new HealthEvent(gameObject, cantidad));
        Debug.Log("Aumento la vida en " + cantidad);
	}


    public void aumentarAirControl(float cantidad)
    {
        GetComponent<MovimientoController>().TiempoSaltoMaximo += cantidad;
    }
    public void decrementalAirControl(float cantidad)
    {
        GetComponent<MovimientoController>().TiempoSaltoMaximo -= cantidad;

    }
    public float getCurrentHealth()
    {
        return this.currentHealth;
    }

	public float getMaxHealth() {
		return this.maxHealth;
	}

    public void AddItem(int itemID)
    {
        totemItems.Add(itemID);
    }

    public List<int> getItemList()
    {
        return totemItems;
    }

   public bool AngelGuardaActivado()
   {
        return this.GetComponentInChildren<Angel>() != null;
    }

    public bool IgluActivado()
    {
        return this.GetComponentInChildren<Iglu>() != null;
    }

    IEnumerator RevivirTotem()
    {
        Debug.Log("Revivir totem");
        Debug.Log(Time.time);
        yield return new WaitForSeconds(2);
        Angel angel = gameObject.GetComponentInChildren<Angel>();
        ResetHealth();
        this.transform.position = angel.GetPosicionValidaTotem();
        angel.ActivarAnimacion();
        angel.IncNumeroUsos();
        Debug.Log(Time.time);
    }

    public bool ColisionaConTerreno()
    {
        return movimiento.ColisionaConTerreno();
    }

    public void ResetHealth()
    {
        this.currentHealth = this.getMaxHealth();
        SendMessage("Heal", new HealthEvent(gameObject, this.getMaxHealth()));
    }

    public bool HotbarLleno()
    {
        return this.getItemList().Count == 3;
    }

    public void EliminarItem(int itemId)
    {
        this.getItemList().Remove(itemId);
    }

    public bool TieneItemActivo(int itemId)
    {
        UnityEngine.Object itemActivo = null;
        switch (itemId)
        {
            case Global.TIPO_OBJETOS.objetoAngel:
                itemActivo = gameObject.GetComponentInChildren<Angel>();
                break;
            case Global.TIPO_OBJETOS.objetoEscudoSimple:
                itemActivo = gameObject.GetComponentInChildren<Escut>();
                break;
            case Global.TIPO_OBJETOS.objetoEscudoDoble:
                itemActivo = gameObject.GetComponentInChildren<EscutDoble>();
                break;
            case Global.TIPO_OBJETOS.objetoIglu:
                itemActivo = gameObject.GetComponentInChildren<Iglu>();
                break;
            default:
                itemActivo = null;
                break;
        }
        return itemActivo != null ? true : false;
    }

    public Vector3 PosicioPrimerModul()
    {
        float yMax = float.MinValue;
        float value = 0;
        foreach(GameObject modul in modulos)
        {
            value = modul.transform.position.y;
            if(yMax < value)
            {
                yMax = value;
            }

        }
        //return this.transform.position+(Vector3.up * yMax);
        //Vector3 pos = new Vector3(this.transform.position.x, yMax, this.transform.position.z);
        return new Vector3(this.transform.position.x, yMax, this.transform.position.z);
    }

    public int GetIDModuloProtegidoIglu()
    {
        float yMax = float.MinValue;
        float value = 0;
        int id = 0;
        foreach (GameObject modul in modulos)
        {
            value = modul.transform.position.y;
            if (yMax < value)
            {
                yMax = value;
                id = modul.GetInstanceID();
            }

        }
        return id;
    }

}