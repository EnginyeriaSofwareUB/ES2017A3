using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeTotem : MonoBehaviour {

	public Transform totemCanvas;
	public GameObject totem1P1, totem2P1, totem3P1, totem4P1;
	public GameObject totem1P2, totem2P2, totem3P2, totem4P2;
	public GameObject currentTotem;


	void Start() {
		totemCanvas.gameObject.SetActive (false);

	}
		
	void Update(){
		if (TeamsData.CurrentTotem != 0) {
			openPopup ();
		}
	}

	public void openPopup(){
		totemCanvas.gameObject.SetActive (true);
		activateTotem (true);

	}

	private void closePopup(){
		totemCanvas.gameObject.SetActive (false);
		activateTotem (false);
	}

	private void activateTotem(bool activated){
		switch (TeamsData.CurrentTotem)
		{
		case 1: //player1 totem1
			totem1P1.SetActive (activated);
			currentTotem = totem1P1;
			break;
		case 2: //p1t2
			totem2P1.SetActive(activated);
			currentTotem = totem2P1;
			break;
		case 3: //p1t3
			totem3P1.SetActive(activated);
			currentTotem = totem3P1;
			break;
		case 4: //p1t4
			totem4P1.SetActive(activated);
			currentTotem = totem4P1;
			break;
		case 5: //p2t1
			totem1P2.SetActive(activated);
			currentTotem = totem1P2;
			break;
		case 6: //p2t2
			totem2P2.SetActive(activated);
			currentTotem = totem2P2;
			break;
		case 7: //p2t3
			totem3P2.SetActive(activated);
			currentTotem = totem3P2;
			break;
		case 8: //p2t4
			totem4P2.SetActive(activated);
			currentTotem = totem4P2;
			break;
		}
	}

	public void buttonAguilaOnClick()
	{
		currentTotem.GetComponent<Totem> ().AddAguilaTotem ();
	}

	public void buttonGorilaOnClick()
	{
		currentTotem.GetComponent<Totem> ().AddGorilaTotem ();
	}

	public void buttonElefanteOnClick()
	{
		currentTotem.GetComponent<Totem> ().AddElefanteTotem ();
	}

	public void buttonTortugaOnClick()
	{
		currentTotem.GetComponent<Totem> ().AddTortugaTotem ();
	}



	// Buttons on click

	public void save(){
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}

	public void cancel(){
		//TODO
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}
}
