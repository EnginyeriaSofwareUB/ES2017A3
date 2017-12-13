using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Environment;
using YounGenTech.HealthScript;

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

    private StateHolder stateHolder;

    public void OnEnable()
    {
        Inventory.ItemEquip += AssignItem;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= AssignItem;
    }

    void AssignItem(Item item)
    {
        GameManager.Instance.AsignarItemTotem(item.itemID);
    }

    void Start()
    {
        this.stateHolder = GetComponent<StateHolder>();
        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");



        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
      

    }

    public bool isInventoryOpen(){
        return inventory.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                this.stateHolder.setInventary();   
                mainInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                this.stateHolder.setPlaying();
                mainInventory.closeInventory();
            }
        }


    }

}
