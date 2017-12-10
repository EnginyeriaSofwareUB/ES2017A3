using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Environment;
using YounGenTech.HealthScript;

public class GestionHotbar : MonoBehaviour
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
        Inventory.ItemConsumed += OnConsumeItem;
    }

    public void OnDisable()
    {
        Inventory.ItemConsumed -= OnConsumeItem;
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
                Debug.Log("Objeto de Cohete salto " + item.itemAttributes[i].attributeValue);
                GameObject cohete = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;

                cohete.transform.GetChild(0).gameObject.AddComponent<Cohete>();
                cohete.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
                cohete.transform.parent = totemActual.transform;
            }

            // Raig: permet moure's més ràpid i tenir més passos límit.

            if (item.itemAttributes[i].attributeName == "Movimiento")
            {
                totemActual.gameObject.GetComponent<MovimientoController>().VelocidadMovimiento += item.itemAttributes[i].attributeValue;
                Debug.Log("Aumento la velocidad de movimiento en  " + item.itemAttributes[i].attributeValue);

            }

            //Coet: propulsa cap a dalt, mantenint pres el botó de salt es manté més en l'aire (air control).
            //  if (item.itemID== Global.TIPO_OBJETOS.objetoCohete)
            //  {
            //ataqueJugadorActual += item.itemAttributes[i].attributeValue;
            //      totemActual.gameObject.transform.position+=new Vector3(0,50,0); 
            //   }
        }

        //Esto en es caso de que ya tengamos activo un angel,escudo, escudo doble o iglu.
        if (totemActual.TieneItemActivo(item.itemID))
        {
            //GameManager.Instance.addTotemItems(totemActual);
            StartCoroutine(GameManager.Instance.AñadirItemHotbar(item.itemID));
            return;
        }
        switch (item.itemID)
        {
            // Bola de teletransport: disparar-la per teletransportar-se a una posició més endavant.
            case Global.TIPO_OBJETOS.objetoTeletransporte:

                //Debug.Log("Objeto de Cohete teletransporte " + item.itemAttributes[i].attributeValue);
                Debug.Log("Objeto de Cohete teletransporte");
                GameObject teletransporte = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;

                teletransporte.transform.GetChild(0).gameObject.AddComponent<Teletransporte>();
                teletransporte.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
                teletransporte.transform.parent = totemActual.transform;
                break;



            case Global.TIPO_OBJETOS.objetoAngel:

                //Debug.Log("Objeto Angel de la Guarda " + item.itemAttributes[i].attributeValue);
                GameObject angel = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;
                angel.GetComponent<SpriteRenderer>().enabled = false;
                //angel.SetActive(false);
                angel.gameObject.AddComponent<Angel>();
                angel.transform.parent = totemActual.transform;
                totemActual.ActivarAngelGuarda();
                break;


            case Global.TIPO_OBJETOS.objetoIglu:

                GameObject iglu = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;

                iglu.transform.GetChild(0).gameObject.AddComponent<Iglu>();
                CircleCollider2D circle = iglu.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
                circle.isTrigger = true;
                iglu.transform.parent = totemActual.transform;
                break;


            case Global.TIPO_OBJETOS.objetoEscudoSimple:

                GameObject escudo = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;
                escudo.transform.position = new Vector3(escudo.transform.position.x + 0.7f, escudo.transform.position.y, escudo.transform.position.z);
                escudo.transform.GetChild(0).gameObject.AddComponent<Escut>();
                BoxCollider2D box = escudo.transform.GetChild(0).gameObject.AddComponent<BoxCollider2D>();
                box.isTrigger = true;
                escudo.transform.parent = totemActual.transform;
                break;


            case Global.TIPO_OBJETOS.objetoEscudoDoble:

                GameObject escudo_doble = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;
                escudo_doble.transform.GetChild(0).gameObject.AddComponent<EscutDoble>();

                BoxCollider2D escudo1 = escudo_doble.transform.GetChild(0).gameObject.AddComponent<BoxCollider2D>();
                CapsuleCollider2D escudo2 = escudo_doble.transform.GetChild(0).gameObject.AddComponent<CapsuleCollider2D>();
                escudo1.isTrigger = true;
                escudo2.isTrigger = true;

                escudo1.offset = new Vector2(-1.17f, -0.5f);
                escudo1.size = new Vector2(0.88f,3.42f);
                escudo1.edgeRadius = 0;

                escudo2.offset = new Vector2(1.18f,-0.5f);
                escudo2.size = new Vector2(0.86f,3.42f);
                escudo2.direction = CapsuleDirection2D.Vertical;
                escudo_doble.transform.parent = totemActual.transform;
                
                break;

		case Global.TIPO_OBJETOS.objetoCohete:
			GameObject cohete = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;
			//cohete.transform.GetChild(0).gameObject.AddComponent<Cohete>();
			cohete.gameObject.AddComponent<Cohete>();
			cohete.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
			cohete.transform.parent = totemActual.transform;
			break;

		case Global.TIPO_OBJETOS.objetoRayo:
			GameObject rayo = Instantiate(item.itemModel, totemActual.transform.position, Quaternion.identity) as GameObject;
			//cohete.transform.GetChild(0).gameObject.AddComponent<Cohete>();
			Debug.Log(rayo.transform.childCount);
			rayo.gameObject.AddComponent<Rayo>();
			rayo.transform.parent = totemActual.transform;
			rayo.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
			break;

        }
        GameManager.Instance.eliminarItemHotbar(item.itemID);

    }
    // Update is called once per frame
    void Update()
    {
    }

}