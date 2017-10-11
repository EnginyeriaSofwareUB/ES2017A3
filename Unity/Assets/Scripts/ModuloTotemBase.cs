using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuloTotemBase : ModuloTotem
{

    public ModuloTotemBase()
    {
        this.ataque = 10;
        this.defensa = 10;
        this.getTipoTotem = TotemType.TOTEM_BASE;
    }

    /*public ModuloTotemBase(float ataque, float defensa) : base(ataque, defensa)
   {
       // Obtengo el recurso
       this.gameObjectTotem = Resources.Load("TotemGorilla") as GameObject;
       // Creo un collider para hacer colisiones
       CircleCollider2D circleCollider2d = this.gameObjectTotem.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
       // Roto 180 grados el mesh
       this.gameObjectTotem.transform.Rotate(new Vector3(0, 180, 0));
   }*/

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
