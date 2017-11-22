using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeTotem : MonoBehaviour {

	public Transform totemCanvas;
	public GameObject totem1P1, totem2P1, totem3P1, totem4P1, totem5P1;
	public GameObject totem1P2, totem2P2, totem3P2, totem4P2, totem5P2;
	public GameObject currentTotem;
	//private int contModulos;


	void Start() {
		totemCanvas.gameObject.SetActive (false);
		//contModulos = 0;

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
		case 9: //p1t5
			totem5P1.SetActive(activated);
			currentTotem = totem5P1;
			break;
		case 10: //p2t5
			totem5P2.SetActive(activated);
			currentTotem = totem5P2;
			break;
		}
	}

	public void buttonAguilaOnClick()
	{
		if(!currentTotem.GetComponent<ContModules>().hasMaxModules())
		{
			currentTotem.GetComponent<Totem> ().AddAguilaTotem ();
			currentTotem.GetComponent<ContModules> ().incrementContModules ();

			/*int[] moduls = currentTotem.GetComponent<ContModules> ().getModules ();
			int cont = currentTotem.GetComponent<ContModules> ().getContModules ();
			moduls [cont] = 1;
			currentTotem.GetComponent<ContModules> ().setModules (moduls);*/
			saveModuleToData (1);
		}

	}

	public void buttonGorilaOnClick()
	{
		if(!currentTotem.GetComponent<ContModules>().hasMaxModules())
		{
			currentTotem.GetComponent<Totem> ().AddGorilaTotem ();
			currentTotem.GetComponent<ContModules> ().incrementContModules ();

			/*int[] moduls = currentTotem.GetComponent<ContModules> ().getModules ();
			int cont = currentTotem.GetComponent<ContModules> ().getContModules ();
			moduls [cont] = 2;
			currentTotem.GetComponent<ContModules> ().setModules (moduls);*/
			saveModuleToData (2);

		}

	}

	public void buttonElefanteOnClick()
	{
		if (!currentTotem.GetComponent<ContModules> ().hasMaxModules ()) 
		{
			currentTotem.GetComponent<Totem> ().AddElefanteTotem ();
			currentTotem.GetComponent<ContModules> ().incrementContModules ();

			/*int[] moduls = currentTotem.GetComponent<ContModules> ().getModules ();
			int cont = currentTotem.GetComponent<ContModules> ().getContModules ();
			moduls [cont] = 3;
			currentTotem.GetComponent<ContModules> ().setModules (moduls);*/
			saveModuleToData (3);

		}
	}

	public void buttonTortugaOnClick()
	{
		if (!currentTotem.GetComponent<ContModules> ().hasMaxModules ()) 
		{
			currentTotem.GetComponent<Totem> ().AddTortugaTotem ();
			currentTotem.GetComponent<ContModules> ().incrementContModules ();

			/*
			int[] moduls = currentTotem.GetComponent<ContModules> ().getModules ();
			int cont = currentTotem.GetComponent<ContModules> ().getContModules ();
			moduls [cont] = 4;
			currentTotem.GetComponent<ContModules> ().setModules (moduls);
			*/
			saveModuleToData (4);


		}
	}



	// Buttons on click

	public void save(){
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}

	public void cancel(){
		//currentTotem.GetComponent<ContModules>().initModules();
		currentTotem.GetComponent<ContModules>().initContModules();
		closePopup ();
		TeamsData.CurrentTotem = 0;
	}



	private void saveModuleToData(int modul){
		//modul: 1 aguila 2 gorila 3 elefante 4 tortuga
		int cont = currentTotem.GetComponent<ContModules> ().getContModules ()-1;
		switch (TeamsData.CurrentTotem) 
		{
		case 1: //player1 totem1
			/*int[] totemModules = TeamsData.ModulesTotem1P1;
			totemModules [cont] = totem;*/
			TeamsData.ModulesTotem1P1 [cont] = modul;
			break;
		case 2: //p1t2
			TeamsData.ModulesTotem2P1 [cont] = modul;			
			break;
		case 3: //p1t3
			TeamsData.ModulesTotem3P1 [cont] = modul;
			break;
		case 4: //p1t4
			TeamsData.ModulesTotem4P1 [cont] = modul;
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
}
