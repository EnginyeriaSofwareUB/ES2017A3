﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTotem : MonoBehaviour
{

	[SerializeField] private int ataqueTotal { get; set; }
	[SerializeField] private int defensaTotal { get; set; }
	[SerializeField] private int precio { get; set; }
	[SerializeField] private float maxHealth=10f;
	[SerializeField] private float currentHealth;

	[SerializeField] private List<GameObject> modulos;

	//[SerializeField] public Hotbar totemHotbar;
	//[SerializeField] public List<Item> totemItems;

	//Manejador del movimiento del jugador
	private MovimientoController movimiento;

	private GameObject gameManager;

	public SimpleTotem(int ataque, int defensa)
	{
		this.ataqueTotal = ataque;
		this.defensaTotal = defensa;
		this.precio = 100;
		//this.totemItems = new List<Item>();
	}



	public SimpleTotem()
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
			//instance.layer = GetComponent<Totem>().gameObject.layer;
			//instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
			modulos.Add(instance);
			break;
		case TotemType.TOTEM_AGUILA:
			instance = Instantiate(Resources.Load("TotemFalcon", typeof(GameObject))) as GameObject;
			instance.AddComponent<ModuloTotemAguila>();
			//instance.layer = GetComponent<Totem>().gameObject.layer;
			//instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
			modulos.Add(instance);
			break;
		case TotemType.TOTEM_GORILA:
			instance = Instantiate(Resources.Load("TotemGorilla", typeof(GameObject))) as GameObject;
			instance.AddComponent<ModuloTotemGorila>();
			//instance.layer = GetComponent<Totem>().gameObject.layer;
			//instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
			modulos.Add(instance);
			break;
		case TotemType.TOTEM_TORTUGA:
			instance = Instantiate(Resources.Load("TotemTurtle", typeof(GameObject))) as GameObject;
			instance.AddComponent<ModuloTotemTortuga>();
			//instance.layer = GetComponent<Totem>().gameObject.layer;
			//instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
			modulos.Add(instance);
			break;
		case TotemType.TOTEM_ELEFANTE:
			instance = Instantiate(Resources.Load("TotemElephant", typeof(GameObject))) as GameObject;
			instance.AddComponent<ModuloTotemElefante>();
			//instance.layer = GetComponent<Totem>().gameObject.layer;
			//instance.tag = GetComponent<Totem>().gameObject.tag + "Module";
			modulos.Add(instance);
			break;
		}

		GameObject lastModuleAdded = modulos[modulos.Count - 1];

		// Sumamos los parámetros de los módulos al totem
		this.ataqueTotal += lastModuleAdded.GetComponent<ModuloTotem>().getAtaque();
		this.defensaTotal += lastModuleAdded.GetComponent<ModuloTotem>().getDefensa();

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
	void Start()
	{
		AddModule(TotemType.TOTEM_BASE);
		//AddModule(TotemType.TOTEM_AGUILA);
		//AddModule(TotemType.TOTEM_GORILA);
		// Hotbar on ficar els items del totem, per no complicarnos serà compartida per tant s'ha de buidar i emplenar amb els items de cada totem al
		// canviar de torn.
		//this.totemHotbar = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<Hotbar>();
		//this.movimiento = GetComponent<MovimientoController>();
		//this.gameManager = GameObject.FindGameObjectWithTag("GameController");
		//this.currentHealth = this.maxHealth;

	}

	// Update is called once per frame
	void Update()
	{
	}

	private void LateUpdate()
	{
		//kill ();
	}

	private void deleteLineRenderer()
	{
		//GameObject drawnLine = transform.Find("Arrow").gameObject;
		//Destroy(drawnLine);
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
		return this.movimiento.isLimitePasos();
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



	public float getCurrentHealth()
	{
		return this.currentHealth;
	}

	public float getMaxHealth() {
		return this.maxHealth;
	}

}