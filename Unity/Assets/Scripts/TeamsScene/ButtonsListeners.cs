using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsListeners : MonoBehaviour {


	public Button totem2Red;
	public Button totem3Red;
	public Button totem4Red;

	public Button totem2Blue;
	public Button totem3Blue;
	public Button totem4Blue;

	private int contRed = 1;
	private int contBlue = 1;


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
		default:
			break;
		};
		contBlue++;
	}
}
