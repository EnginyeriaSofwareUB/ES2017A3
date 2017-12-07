using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOverPlatform : MonoBehaviour {
    private GameObject lastParent;

    void Start()
    {
        //lastParent = transform.parent.transform.parent.gameObject;
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent.transform.parent = col.transform;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent.transform.parent = lastParent.transform;

        }
    }
}
