using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour {
	private GameObject totemObject;
	private Totem totem;
	private float vida;

	// Use this for initialization
	void Start () {
		totemObject = transform.parent.gameObject.transform.parent.gameObject;
		totem = totemObject.GetComponent<Totem> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		vida = totem.getVida ();
		print (vida);
		//cambiamos medida barra
		if (vida < 5) {
			print ("Cambiamos color");
		}
	}
}
