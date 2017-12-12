using Assets.Scripts.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public GameObject esphere;

    static ItemDataBaseList inventoryItemList;
    // Numero aleatorio utilizado para obtener un número a partir de todos los items en la base de datos
    int numeroAleatorio;

    // Use this for initialization
    void Start () {
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
        numeroAleatorio = Random.Range(1, inventoryItemList.itemList.Count - 1);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            /*GameObject itemGameObject = (GameObject)Instantiate(inventoryItemList.itemList[numeroAleatorio].itemModel);
            PickUpItem item = itemGameObject.AddComponent<PickUpItem>();
            item.item = inventoryItemList.itemList[numeroAleatorio];
            itemGameObject.gameObject.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
            PickUpItem eliinar = itemGameObject.GetComponent<PickUpItem>();
            itemGameObject.AddComponent<CogerObjeto>();
            itemGameObject.AddComponent<Rigidbody2D>();
            itemGameObject.GetComponent<CogerObjeto>().item = item.item;
            Instantiate(itemGameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);*/

			Item item = inventoryItemList.itemList[numeroAleatorio];
            GameObject itemGameObject = (GameObject)Instantiate(item.itemModel,transform.position,Quaternion.identity);
            //itemGameObject.AddComponent<PickUpItem>();
            //itemGameObject.GetComponent<PickUpItem>().item = item;
            itemGameObject.AddComponent<CogerObjeto>();
            itemGameObject.AddComponent<Rigidbody2D>();
            itemGameObject.AddComponent<CircleCollider2D>();
            itemGameObject.GetComponent<CogerObjeto>().item = item;

            //Ponemos el efecto asociado a ese objeto
            setEfectoObjetoById(itemGameObject, item.itemID);

            Destroy(gameObject);
        }
    }

    private void setEfectoObjetoById(GameObject objeto,int id)
    {
        switch (id)
        {
            case Global.TIPO_OBJETOS.objetoBotiquin:
                GameObject efectoBotiquin = Instantiate(Resources.Load("GreenCore"), objeto.transform.position, Quaternion.identity) as GameObject;
                efectoBotiquin.transform.parent = objeto.transform;
                break;
            case Global.TIPO_OBJETOS.objetoVitamina:
                //TODO: Queda poner las particulas necesarias. Preguntar a albert 
                GameObject efectoVitamina = Instantiate(Resources.Load("GreenCore"),objeto.transform.position, Quaternion.identity) as GameObject;
                efectoVitamina.transform.parent = objeto.transform;
                break;

            case Global.TIPO_OBJETOS.objetoCohete:
				GameObject efectoCohete = Instantiate(Resources.Load("Fx_OilTrailHIGH_Root"), objeto.transform.position, Quaternion.identity) as GameObject;
                break;

            case Global.TIPO_OBJETOS.objetoTeletransporte:
                GameObject efectoTeletransporte = Instantiate(Resources.Load("ErekiBall2"), objeto.transform.position, Quaternion.identity) as GameObject;
                efectoTeletransporte.transform.parent = objeto.transform;
                break;


        }
    }
}
