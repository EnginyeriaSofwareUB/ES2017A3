using UnityEngine;
using System.Collections;

public class Cohete : MonoBehaviour
{
    private GameObject efectoCohete;
	private GameManager gameManager;
	private Totem totem;

    // Use this for initialization
    void Start()
    {

        //GameObject efectoCohete = Instantiate(Resources.Load("Fx_OilTrailHIGH_Root"), this.transform.position, Quaternion.identity) as GameObject;

        //efectoCohete.transform.parent = this.transform;

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
            Debug.Log("Colision cohete");
            Destroy(efectoCohete);
        }
    }
    // Update is called once per frame
    void Update()
    {
		if (EfectoHaTerminado())
		{
			desactivarEfecto ();
			Destroy(gameObject);
		}
    }


	private bool EfectoHaTerminado()
	{

		return (gameManager.getActualTotem() != totem);
	}

	private void activarEfecto(){

		totem.GetComponent<MovimientoController>().setTiempoSaltoMax (0.75f);
		totem.GetComponent<MovimientoController>().setFuerzaSalto (25f);
	}

	private void desactivarEfecto(){
		totem.GetComponent<MovimientoController>().setTiempoSaltoMax (0.5f);
		totem.GetComponent<MovimientoController>().setFuerzaSalto (20f);
	}


}
