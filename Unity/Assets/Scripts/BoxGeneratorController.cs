using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGeneratorController : MonoBehaviour {
    public GameObject box;
    public int boxTurn = 2;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddBox()
    {
        Instantiate(box, transform.position, Quaternion.identity);
    }
}
