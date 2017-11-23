using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsListeners : MonoBehaviour {


	public Button totem2Red;
	public Button totem3Red;
	public Button totem4Red;
	public Button totem5Red;

	public Button totem2Blue;
	public Button totem3Blue;
	public Button totem4Blue;
	public Button totem5Blue;

	private int contRed = 1;
	private int contBlue = 1;

	//public GameObject totem; 
	//public CustomizeTotem obj;


	/*
	public void setTotem(GameObject t){
		totem = t;
	}*/

	public void fight()
	{
		TeamsData.PlayersRed = contRed;
		TeamsData.PlayersBlue = contBlue;
		SceneManager.LoadScene ("JuegoScene");
	}

	public void back()
	{
		SceneManager.LoadScene ("MenuScene");
	}

	public void addNewPlayerRed()
	{
		switch (contRed) {
		case 1:
			totem2Red.gameObject.SetActive (true);
			break;
		case 2:
			totem3Red.gameObject.SetActive (true);
			break;
		case 3:
			totem4Red.gameObject.SetActive (true);
			break;
		case 4:
			totem5Red.gameObject.SetActive (true);
			break;
		default:
			break;
		};
		contRed++;
	}

	public void addNewPlayerBlue()
	{
		switch (contBlue) {
		case 1:
			totem2Blue.gameObject.SetActive (true);
			break;
		case 2:
			totem3Blue.gameObject.SetActive (true);
			break;
		case 3:
			totem4Blue.gameObject.SetActive (true);
			break;
		case 4:
			totem5Blue.gameObject.SetActive (true);
			break;
		default:
			break;
		};
		contBlue++;
	}




	//totems

	public void buttonCustomizeTotem1P1(){
		TeamsData.CurrentTotem = 1;
	}

	public void buttonCustomizeTotem2P1(){
		TeamsData.CurrentTotem = 2;
	}

	public void buttonCustomizeTotem3P1(){
		TeamsData.CurrentTotem = 3;
	}

	public void buttonCustomizeTotem4P1(){
		TeamsData.CurrentTotem = 4;
	}

	public void buttonCustomizeTotem5P1(){
		TeamsData.CurrentTotem = 9;
	}

	public void buttonCustomizeTotem1P2(){
		TeamsData.CurrentTotem = 5;
	}

	public void buttonCustomizeTotem2P2(){
		TeamsData.CurrentTotem = 6;
	}

	public void buttonCustomizeTotem3P2(){
		TeamsData.CurrentTotem = 7;
	}

	public void buttonCustomizeTotem4P2(){
		TeamsData.CurrentTotem = 8;
	}

	public void buttonCustomizeTotem5P2(){
		TeamsData.CurrentTotem = 10;
	}
		
}
