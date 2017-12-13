using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OurCloseInventory : MonoBehaviour, IPointerDownHandler
{

    Inventory inv;

    StateHolder stateHolder;

    private bool executing = false;

    void Start()
    {
        inv = transform.parent.GetComponent<Inventory>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject empty = new GameObject();
            empty.AddComponent<SetPlay>();
            Destroy(empty, 1.0f);
            inv.closeInventory();
        }
    }
}
