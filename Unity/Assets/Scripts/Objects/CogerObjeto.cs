using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class CogerObjeto : MonoBehaviour
{
    public Item item;
    private Inventory inventario;
    private GameObject _player;
    // Use this for initialization
    private GameManager gestorJuego;


    private GameObject inventarioGameObject;
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public  void OnCollisionEnter2D(Collision2D collision)
    {
        // La primera condición es sea un totem del primer jugador
        bool primeraCondicion = collision.gameObject.layer == Global.Capas.totemsPrimerJugador;
        // La segunda condición es que sea un totem del contrincante
        bool segundaCondicion = collision.gameObject.layer == Global.Capas.totemsSegundoJugador;
        // Condición final, True si la colisión la ha provocado un totem
        bool condicionFinal = primeraCondicion || segundaCondicion;

        if (condicionFinal)
        {
            GestionInventario playerInventario = collision.transform.parent.GetComponent<GestionInventario>();
            inventario = playerInventario.inventory.GetComponent<Inventory>();
            inventario.addItemToInventory(item.itemID, item.itemValue);
            inventario.updateItemList();
            inventario.stackableSettings();
            GameManager.Instance.guardarItem(item);
            Destroy(this.gameObject);
                    
            }
        }
    }
