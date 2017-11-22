using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class Vitamina : HealingItem
{
    private int NUM_USOS_VITAMINA = 10;

    public override void applyEffect()
    {
        throw new System.NotImplementedException();
    }
    // El item es de curación
    public override ItemType GetItemType()
    {
        return ItemType.Curació;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        // La primera condición es sea un totem del primer jugador
        bool primeraCondicion = collision.gameObject.layer == Global.Capas.totemsPrimerJugador;
        // La segunda condición es que sea un totem del contrincante
        bool segundaCondicion = collision.gameObject.layer == Global.Capas.totemsSegundoJugador;
        // Condición final, True si la colisión la ha provocado un totem
        bool condicionFinal = primeraCondicion || segundaCondicion;

        if (condicionFinal)
        {
            // Obtengo el jugador
            Totem totemJugador = collision.gameObject.GetComponent<Totem>();
            totemJugador.aumentarVida(this.heal);

            // Destruyo la vitamina
            if (this.useCounter > this.uses)
                Destroy(gameObject);
        }
    }

    public override void removeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void setHealingPower()
    {
        this.heal = 5;
    }

    protected override void setItemUses()
    {
        this.uses = NUM_USOS_VITAMINA;
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
