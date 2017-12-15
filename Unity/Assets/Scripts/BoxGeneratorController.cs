using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGeneratorController : MonoBehaviour {
    public GameObject box;
    public bool boxTurn;
    public PlatformController platform;

    // Use this for initialization
    void Start () {
        boxTurn = Random.Range(0, 2) == 1;
        //boxTurn = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddBox()
    {
        Instantiate(box, new Vector3(Random.Range(platform.getMinHorizontalValue(), platform.getMaxHorizontalValue()),transform.position.y,transform.position.z), Quaternion.identity);
    }
}
