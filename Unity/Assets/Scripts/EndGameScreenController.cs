using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndGameScreenController : MonoBehaviour {

	public Transform endGameCanvas; 
	//private StateHolder stateHolder;
	private GameManager gameManager;
	public Text winnerTxt;


	void Start() {
		//this.stateHolder = GetComponent<StateHolder>();
		this.gameManager = GetComponent<GameManager>();
		fillWinnerText ();
		endGameCanvas.gameObject.SetActive (false);
	}



	private void fillWinnerText(){
		if (gameManager.isEmptyList (GameManager.LISTA_TOTEMS.LISTA_JUGADOR)) {
			winnerTxt.text = "Player 2 wins!";
		} else {
			winnerTxt.text = "Player 1 wins!";
		}
	}





	// BUTTON LISTENERS
	public void PlayAgain(){
		SceneManager.LoadScene ("JuegoScene");

	}

	public void BackToMenu(){
		SceneManager.LoadScene ("MenuScene");
	}
}
