using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeTotem : MonoBehaviour {

	public Transform totemCanvas;
	public GameObject totem1P1, totem2P1, totem3P1, totem4P1, totem5P1;
	public GameObject totem1P2, totem2P2, totem3P2, totem4P2, totem5P2;
	public GameObject currentTotem;
	private bool isPlayerRed;
	public DineroController dinero;
	public GameObject dineros;


	void Start() {
		initModules ();
		totemCanvas.gameObject.SetActive (false);
		dinero = dineros.GetComponent<DineroController>();
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
			isPlayerRed = true;
			break;
		case 2: //p1t2
			totem2P1.SetActive(activated);
			currentTotem = totem2P1;
			isPlayerRed = true;
			break;
		case 3: //p1t3
			totem3P1.SetActive(activated);
			currentTotem = totem3P1;
			isPlayerRed = true;
			break;
		case 4: //p1t4
			totem4P1.SetActive(activated);
			currentTotem = totem4P1;
			isPlayerRed = true;
			break;
		case 5: //p2t1
			totem1P2.SetActive(activated);
			currentTotem = totem1P2;
			isPlayerRed = false;
			break;
		case 6: //p2t2
			totem2P2.SetActive(activated);
			currentTotem = totem2P2;
			isPlayerRed = false;
			break;
		case 7: //p2t3
			totem3P2.SetActive(activated);
			currentTotem = totem3P2;
			isPlayerRed = false;
			break;
		case 8: //p2t4
			totem4P2.SetActive(activated);
			currentTotem = totem4P2;
			isPlayerRed = false;
			break;
		case 9: //p1t5
			totem5P1.SetActive(activated);
			currentTotem = totem5P1;
			isPlayerRed = true;
			break;
		case 10: //p2t5
			totem5P2.SetActive(activated);
			currentTotem = totem5P2;
			isPlayerRed = false;
			break;
		}
	}

	public void buttonAguilaOnClick()
	{
		if(!currentTotem.GetComponent<ContModules>().hasMaxModules() )
		{
			if (enoughMoney ()) {
				currentTotem.GetComponent<SimpleTotem> ().AddAguilaTotem ();
				currentTotem.GetComponent<ContModules> ().incrementContModules ();
				saveModuleToData (1);
				pay ();
			} else {
				string p;
				if (isPlayerRed) {
					p = "RED";
				} else {
					p = "BLUE";
				}
				dinero.showWarning (p, "ALLIGATOR MODULE");
			}
		}

	}

	public void buttonGorilaOnClick()
	{
		if(!currentTotem.GetComponent<ContModules>().hasMaxModules() )
		{
			if (enoughMoney ()) {
				currentTotem.GetComponent<SimpleTotem> ().AddGorilaTotem ();
				currentTotem.GetComponent<ContModules> ().incrementContModules ();
				saveModuleToData (2);
				pay ();
			} else {
				string p;
				if (isPlayerRed) {
					p = "RED";
				} else {
					p = "BLUE";
				}
				dinero.showWarning (p, "GORILLA MODULE");
			}

		}

	}

	public void buttonElefanteOnClick()
	{
		if (!currentTotem.GetComponent<ContModules> ().hasMaxModules ()) 
		{
			if (enoughMoney ()) {
				currentTotem.GetComponent<SimpleTotem> ().AddElefanteTotem ();
				currentTotem.GetComponent<ContModules> ().incrementContModules ();
				saveModuleToData (3);
				pay ();
			} else {
				string p;
				if (isPlayerRed) {
					p = "RED";
				} else {
					p = "BLUE";
				}
				dinero.showWarning (p, "ELEPHANT MODULE");
			}
				
		}
	}

	public void buttonTortugaOnClick()
	{
		if (!currentTotem.GetComponent<ContModules> ().hasMaxModules () ) 
		{
			if (enoughMoney ()) {
				currentTotem.GetComponent<SimpleTotem> ().AddTortugaTotem ();
				currentTotem.GetComponent<ContModules> ().incrementContModules ();
				saveModuleToData (4);
				pay ();
			} else {
				string p;
				if (isPlayerRed) {
					p = "RED";
				} else {
					p = "BLUE";
				}
				dinero.showWarning (p, "TURTLE MODULE");
			}
		}
	}

	private bool enoughMoney(){
		if (isPlayerRed) {
			return dinero.redHasEnoughMoneyForModule ();
		} else {
			return dinero.blueHasEnoughMoneyForModule ();
		}
	}

	private void pay(){
		if (isPlayerRed) {
			dinero.redBuysModule ();
			Debug.Log ("RED"+dinero.getRedMoney());
		} else {
			dinero.blueBuysModule ();
			Debug.Log ("BLUE"+dinero.getBlueMoney());
		}
		dinero.updateTexts ();
	}



	// Buttons on click

	public void save(){
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}

	public void cancel(){
		currentTotem.GetComponent<ContModules>().initContModules();
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}



	private void saveModuleToData(int modul){
		//modul: 1 aguila 2 gorila 3 elefante 4 tortuga
		int cont = currentTotem.GetComponent<ContModules> ().getContModules ()-1;
		int[] totemModules;
		switch (TeamsData.CurrentTotem) 
		{
		case 1: //player1 totem1
			totemModules = TeamsData.ModulesTotem1P1;
			totemModules [cont] = modul;
			TeamsData.ModulesTotem1P1 = totemModules;
			//TeamsData.ModulesTotem1P1 [cont] = modul;
			break;
		case 2: //p1t2
			totemModules = TeamsData.ModulesTotem2P1;
			totemModules [cont] = modul;
			TeamsData.ModulesTotem2P1 = totemModules;
			//TeamsData.ModulesTotem2P1 [cont] = modul;			
			break;
		case 3: //p1t3
			totemModules = TeamsData.ModulesTotem3P1;
			totemModules [cont] = modul;
			TeamsData.ModulesTotem3P1 = totemModules;
			//TeamsData.ModulesTotem3P1 [cont] = modul;
			break;
		case 4: //p1t4
			totemModules = TeamsData.ModulesTotem4P1;
			totemModules [cont] = modul;
			TeamsData.ModulesTotem4P1 = totemModules;
			//TeamsData.ModulesTotem4P1 [cont] = modul;
			break;
		case 5: //p2t1
			TeamsData.ModulesTotem1P2 [cont] = modul;
			break;
		case 6: //p2t2
			TeamsData.ModulesTotem2P2 [cont] = modul;
			break;
		case 7: //p2t3
			TeamsData.ModulesTotem3P2 [cont] = modul;
			break;
		case 8: //p2t4
			TeamsData.ModulesTotem4P2 [cont] = modul;
			break;
		case 9: //p1t5
			TeamsData.ModulesTotem5P1 [cont] = modul;
			break;
		case 10: //p2t5
			TeamsData.ModulesTotem5P2 [cont] = modul;
			break;
	}

	}


	private void initModules(){
		TeamsData.ModulesTotem1P1 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem2P1 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem3P1 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem4P1 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem5P1 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem1P2 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem2P2 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem3P2 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem4P2 = new int[] { 0, 0, 0, 0 };
		TeamsData.ModulesTotem5P2 = new int[] { 0, 0, 0, 0 };
	}
}
