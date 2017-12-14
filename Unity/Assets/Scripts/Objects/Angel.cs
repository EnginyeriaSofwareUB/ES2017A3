using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class Angel : MonoBehaviour
{
    private int rondaInicial;
    private int numeroUsos;
    private GameManager gameManager;
    public Vector3 posicionValidaTotem;
    private Totem totem;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rondaInicial = gameManager.GetRondaActual();
        numeroUsos = 0;
        totem = GetComponentInParent<Totem>();
        posicionValidaTotem = totem.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(EfectoHaTerminado())
        {
            Destroy(gameObject,2f);
            //totem.DesactivarAngelGuarda();
        }
        /*if (totem.ColisionaConTerreno())
        {
            posicionValidaTotem = totem.transform.position;
        }*/
    }

    private bool EfectoHaTerminado()
    {
        return ((gameManager.GetRondaActual() - rondaInicial) == Global.MAX_RONDA_ITEM.ANGEL) || (numeroUsos == Global.MAX_USO_ITEM.ANGEL);
    }

    public void IncNumeroUsos()
    {
        numeroUsos += 1;
    }

    public void ActivarAnimacion()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public Vector3 GetPosicionValidaTotem()
    {
        return posicionValidaTotem;
    }

}
