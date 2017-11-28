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

        GameObject itemGameObject = (GameObject)Instantiate(inventoryItemList.itemList[numeroAleatorio].itemModel);
        PickUpItem item = itemGameObject.AddComponent<PickUpItem>();
        item.item = inventoryItemList.itemList[numeroAleatorio];

        itemGameObject.gameObject.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
   



        PickUpItem eliinar = itemGameObject.GetComponent<PickUpItem>();
        itemGameObject.AddComponent<CogerObjecto>();
        itemGameObject.AddComponent<Rigidbody2D>();
        itemGameObject.GetComponent<CogerObjecto>().item = item;
        Instantiate(itemGameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);

        if (collision.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
            //Alliberar objecte o trampa

        }
    }
}
