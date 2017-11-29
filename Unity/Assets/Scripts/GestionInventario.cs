using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Environment;

public class GestionInventario : MonoBehaviour
{
    private Totem totemJugador;
    public GameObject inventory;
    private Inventory mainInventory;
    private Tooltip toolTip;

    private InputManager inputManagerDatabase;

    private float ataqueJugadorActual;
    private float vidaJugadorActual;
    private float previoAtaqueJugador;
    private float previoVidaJugador;

    int normalSize = 3;
    private GameManager gestorJuego;
    public void OnEnable()
    {
        Inventory.ItemEquip += OnBackpack;
        Inventory.UnEquipItem += UnEquipBackpack;


        Inventory.ItemConsumed += OnConsumeItem;

        Inventory.ItemEquip += EquipWeapon;
        Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnBackpack;
        Inventory.UnEquipItem -= UnEquipBackpack;


        Inventory.ItemConsumed -= OnConsumeItem;


        Inventory.UnEquipItem -= UnEquipWeapon;
        Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //add the weapon if you unequip the weapon
        }
    }

    void UnEquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //delete the weapon if you unequip the weapon
        }
    }

    void OnBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
        {
            for (int i = 0; i < item.itemAttributes.Count; i++)
            {
                if (mainInventory == null)
                    mainInventory = inventory.GetComponent<Inventory>();
                mainInventory.sortItems();
                if (item.itemAttributes[i].attributeName == "Slots")
                    changeInventorySize(item.itemAttributes[i].attributeValue);
            }
        }
    }

    void UnEquipBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
            changeInventorySize(normalSize);
    }

    void changeInventorySize(int size)
    {
        dropTheRestItems(size);

        if (mainInventory == null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (size == 3)
        {
            mainInventory.width = 3;
            mainInventory.height = 1;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        if (size == 6)
        {
            mainInventory.width = 3;
            mainInventory.height = 2;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 12)
        {
            mainInventory.width = 4;
            mainInventory.height = 3;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 16)
        {
            mainInventory.width = 4;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 24)
        {
            mainInventory.width = 6;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
    }

    void dropTheRestItems(int size)
    {
        if (size < mainInventory.ItemsInInventory.Count)
        {
            for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
            {
                GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
                dropItem.AddComponent<PickUpItem>();
                dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            }
        }
    }

    void Start()
    {
        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");



        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
      

    }



    public void OnConsumeItem(Item item)
    {
        Totem totemActual = GameManager.Instance.totemActual;
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {

            if (item.itemAttributes[i].attributeName == "Vida")
            {
                totemActual.aumentarVida(item.itemAttributes[i].attributeValue);
                Debug.Log("Objeto de vida");
                
            }


            if (item.itemAttributes[i].attributeName == "Ataque")
            {
                ataqueJugadorActual += item.itemAttributes[i].attributeValue;
            }

            if (item.itemAttributes[i].attributeName == "Cohete")
            {
                totemActual.GetComponent<MovimientoController>().saltar(item.itemAttributes[i].attributeValue);
                Debug.Log("Objeto de Cohete salto "+ item.itemAttributes[i].attributeValue);           
                GameObject cohete = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;

                cohete.transform.GetChild(0).gameObject.AddComponent<Cohete>();
                cohete.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
                cohete.transform.parent = totemActual.transform;
            }

            //Coet: propulsa cap a dalt, mantenint pres el botó de salt es manté més en l'aire (air control).
            //  if (item.itemID== Global.TIPO_OBJETOS.objetoCohete)
            //  {
            //ataqueJugadorActual += item.itemAttributes[i].attributeValue;
            //      totemActual.gameObject.transform.position+=new Vector3(0,50,0); 
            //   }

            // Bola de teletransport: disparar-la per teletransportar-se a una posició més endavant.
            if (item.itemID == Global.TIPO_OBJETOS.objetoTeletransporte)
            {
                Debug.Log("Objeto de Cohete teletransporte " + item.itemAttributes[i].attributeValue);
                GameObject teletransporte = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;

                teletransporte.transform.GetChild(0).gameObject.AddComponent<Teletransporte>();
                teletransporte.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
                teletransporte.transform.parent = totemActual.transform;
         
            }
            // Raig: permet moure's més ràpid i tenir més passos límit.

            if (item.itemAttributes[i].attributeName == "Movimiento")
            {
                totemActual.gameObject.GetComponent<MovimientoController>().VelocidadMovimiento += item.itemAttributes[i].attributeValue;
                Debug.Log("Aumento la velocidad de movimiento en  " + item.itemAttributes[i].attributeValue);

            }
        }
        GameManager.Instance.eliminarItem(item);

    }




    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                mainInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                mainInventory.closeInventory();
            }
        }


    }

}
