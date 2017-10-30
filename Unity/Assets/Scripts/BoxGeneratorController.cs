using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGeneratorController : MonoBehaviour {
    public GameObject box;
    public int boxTurn = 2;
    public PlatformController platform;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddBox()
    {
        Instantiate(box, new Vector3(Random.Range(platform.getMinHorizontalValue(), platform.getMaxHorizontalValue()),transform.position.y,transform.position.z), Quaternion.identity);
    }
}
