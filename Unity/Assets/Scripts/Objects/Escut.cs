using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class Escut : MonoBehaviour {

    private int rondaInicial;
    private int numeroUsos;
    private GameManager gameManager;
    private Totem totem;
    private int capaBala;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rondaInicial = gameManager.GetRondaActual();
        numeroUsos = 0;
        totem = GetComponentInParent<Totem>();
        capaBala = totem.gameObject.layer == Global.Capas.totemsPrimerJugador ? Global.Capas.balaPrimerJugador : Global.Capas.balaSegundoJugador;
    }

    // Update is called once per frame
    void Update()
    {
        if (EfectoHaTerminado())
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private bool EfectoHaTerminado()
    {
        return (gameManager.GetRondaActual() - rondaInicial == Global.MAX_RONDA_ITEM.ESCUT) || (numeroUsos == Global.MAX_USO_ITEM.ESCUT);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Global.WEAPON && (capaBala != collision.gameObject.layer))
        {
            numeroUsos += 1;
            Destroy(collision.gameObject);
        }
    }
}
