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


        GameObject instance = Instantiate(Resources.Load("TotemBase", typeof(GameObject))) as GameObject;


        this.MeshTotem = instance.transform;

    }

    public void Awake()
    {

    }
    /*public ModuloTotemBase(float ataque, float defensa) : base(ataque, defensa)
   {
      
   }*/

    // Use this for initialization
    void Start()
    {
        // Obtengo el recurso
        //this.meshTotem = Resources.Load("TotemGorilla") as GameObject;
        // Creo un collider para hacer colisiones
        //CircleCollider2D circleCollider2d = this.meshTotem.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        // Roto 180 grados el mesh
        // this.meshTotem.transform.Rotate(new Vector3(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}