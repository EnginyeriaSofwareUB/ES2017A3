using UnityEngine;
using System.Collections;

public class Rayo : MonoBehaviour
{
	private GameObject efectoRayo;
	private GameManager gameManager;
	private Totem totem;

	// Use this for initialization
	void Start()
	{
		efectoRayo = Instantiate(Resources.Load("Spark"), this.transform.position, Quaternion.identity) as GameObject;
		efectoRayo.transform.parent = this.transform;
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		totem = GetComponentInParent<Totem>();
		activarEfecto ();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		string tag = collision.collider.tag;
		if (tag == "TerrainObject")
		{
			Destroy(gameObject);
			Destroy(efectoRayo);
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (EfectoHaTerminado())
		{
			desactivarEfecto ();
			Destroy(gameObject);
			Destroy(efectoRayo);
		}
	}


	private bool EfectoHaTerminado()
	{
		return (gameManager.getActualTotem() != totem);
	}

	private void activarEfecto(){
		totem.GetComponent<MovimientoController>().setDistanciaLimite (30f);
		totem.GetComponent<MovimientoController>().setVelocidadMovimiento (35f);
	}

	private void desactivarEfecto(){
		totem.GetComponent<MovimientoController>().setDistanciaLimite (20f);
		totem.GetComponent<MovimientoController>().setVelocidadMovimiento (25f);
	}


}
