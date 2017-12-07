using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class DineroController : MonoBehaviour {

	private int moneyRed;
	private int moneyBlue; 
	public Text redTxt;
	public Text blueTxt;
	public Text warniningTxt;
	public GameObject warningTxt;

	void Start () {
		moneyRed = 600;
		moneyBlue = 600;
	}


	public bool redHasEnoughMoneyForModule(){
		return moneyRed > 40; 
	}

	public bool redHasEnoughMoneyForTotem(){
		return moneyRed > 100;
	}

	public bool blueHasEnoughMoneyForModule(){
		return moneyBlue > 40;
	}

	public bool blueHasEnoughMoneyForTotem(){
		return moneyBlue > 100; 
	}

	public void decreaseMoneyRed(int am){
		moneyRed -= am;
	}

	public void decreaseMoneyBlue(int am){
		moneyBlue -= am;
	}

	public void redBuysTotem(){
		moneyRed -= 100;
	}

	public void blueBuysTotem(){
		moneyBlue -= 100;
	}

	public void redBuysModule(){
		moneyRed -= 40;
	}

	public void blueBuysModule(){
		moneyBlue -= 40;
	}

	public int getRedMoney(){
		return moneyRed;
	}

	public int getBlueMoney(){
		return moneyBlue;
	}


	public void updateTexts(){
		redTxt.text = "Money left:" + moneyRed;
		blueTxt.text = "Money left:" + moneyBlue;
	}

	public void showWarning(string team, string product){
		warningTxt.SetActive (true);
		warniningTxt.text = team + " team does not have enough money to buy a new " + product;
	}

	public void hideWarning(){
		warningTxt.SetActive (false);
	}

}
