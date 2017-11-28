using UnityEngine;
using System.Collections;
using Assets.Scripts.Environment;

public class CogerObjeto : MonoBehaviour
{
    public Item item;
    private Inventory _inventory;
    private GameObject _player;
    // Use this for initialization
    private GameManager gestorJuego;
    void Start()
    {
        gestorJuego = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

       // _player = gestorJuego.totemActual.gameObject;
       // if (_player != null)
       //     _inventory = _player.GetComponent<GestionInventario>().inventory.GetComponent<Inventory>();
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
            // Obtengo el jugador
            _player = collision.gameObject;
            _inventory = _player.GetComponentInParent<PlayerInventory>().inventory.GetComponent<Inventory>();
            _player.GetComponent<Totem>().AddItem(item);
            //_inventory.deleteAllItems();
           // _inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();

            _inventory.addItemToInventory(item.itemID, item.itemValue);
            _inventory.updateItemList();
            _inventory.stackableSettings();
            Destroy(this.gameObject);
                    
            }
        }
    }
