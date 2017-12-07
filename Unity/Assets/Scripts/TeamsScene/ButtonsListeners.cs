using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsListeners : MonoBehaviour {

	public Button totem1Red;
	public Button totem2Red;
	public Button totem3Red;
	public Button totem4Red;
	public Button totem5Red;

	public Button totem1Blue;
	public Button totem2Blue;
	public Button totem3Blue;
	public Button totem4Blue;
	public Button totem5Blue;

	public Sprite totem2RedSprite;
	public Sprite totem1RedSprite;
	public Sprite totem3RedSprite;
	public Sprite totem4RedSprite;
	public Sprite totem5RedSprite;

	public Sprite totem1BlueSprite;
	public Sprite totem2BlueSprite;
	public Sprite totem3BlueSprite;
	public Sprite totem4BlueSprite;
	public Sprite totem5BlueSprite;


	private int contRed = 1;
	private int contBlue = 1;

	public GameObject totem; 
	public GameObject dinero;
	public CustomizeTotem obj;
	private DineroController dineroController;


	void Start () {
		dineroController = dinero.GetComponent<DineroController>();
	}

	public void setTotem(GameObject t){
		totem = t;
	}

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
		if (dineroController.redHasEnoughMoneyForTotem ()) {
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
			}
			;
			dineroController.redBuysTotem ();
			Debug.Log ("Red"+dineroController.getRedMoney());
			contRed++;
		} else {
			//TODO display text not enough money 
			Debug.Log("not enough money");
		}


	}

	public void addNewPlayerBlue()
	{
		if (dineroController.blueHasEnoughMoneyForTotem ()) {
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
			}
			;
			dineroController.blueBuysTotem ();
			Debug.Log ("Blue"+dineroController.getBlueMoney());
			contBlue++;
		} else {
			//TODO show text not enough money
			Debug.Log("not enough money");
		}

	}




	//totems

	public void buttonCustomizeTotem1P1(){
		TeamsData.CurrentTotem = 1;
		totem1Red.GetComponent<Image>().sprite = totem1RedSprite;

	}

	public void buttonCustomizeTotem2P1(){
		TeamsData.CurrentTotem = 2;
		totem2Red.GetComponent<Image>().sprite = totem2RedSprite;
	}

	public void buttonCustomizeTotem3P1(){
		TeamsData.CurrentTotem = 3;
		totem3Red.GetComponent<Image>().sprite = totem3RedSprite;
	}

	public void buttonCustomizeTotem4P1(){
		TeamsData.CurrentTotem = 4;
		totem4Red.GetComponent<Image>().sprite = totem4RedSprite;
	}

	public void buttonCustomizeTotem5P1(){
		TeamsData.CurrentTotem = 9;
		totem5Red.GetComponent<Image>().sprite = totem5RedSprite;
	}

	public void buttonCustomizeTotem1P2(){
		TeamsData.CurrentTotem = 5;
		totem1Blue.GetComponent<Image>().sprite = totem1BlueSprite;
	}

	public void buttonCustomizeTotem2P2(){
		TeamsData.CurrentTotem = 6;
		totem2Blue.GetComponent<Image>().sprite = totem2BlueSprite;
	}

	public void buttonCustomizeTotem3P2(){
		TeamsData.CurrentTotem = 7;
		totem3Blue.GetComponent<Image>().sprite = totem3BlueSprite;
	}

	public void buttonCustomizeTotem4P2(){
		TeamsData.CurrentTotem = 8;
		totem4Blue.GetComponent<Image>().sprite = totem4BlueSprite;
	}

	public void buttonCustomizeTotem5P2(){
		TeamsData.CurrentTotem = 10;
		totem5Blue.GetComponent<Image>().sprite = totem5BlueSprite;
	}
		
}
