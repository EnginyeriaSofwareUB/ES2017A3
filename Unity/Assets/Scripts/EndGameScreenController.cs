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
	public Text totemsP1Txt;
	public Text totemsP2Txt;


	void Start() {
		//this.stateHolder = GetComponent<StateHolder>();
		this.gameManager = GetComponent<GameManager>();
		endGameCanvas.gameObject.SetActive (false);
	}


	public void fillWinnerText(){
		//Debug.Log("EndGameScreenController :: lista jugador empty = "+gameManager.isEmptyList (GameManager.LISTA_TOTEMS.LISTA_JUGADOR));
		//Debug.Log("EndGameScreenController :: lista contrinc empty = "+gameManager.isEmptyList (GameManager.LISTA_TOTEMS.LISTA_CONTRICANTE));
		if (gameManager.isEmptyList (GameManager.LISTA_TOTEMS.LISTA_JUGADOR)) {
			winnerTxt.text = "BLUE PLAYER WINS!";
		} else {
			winnerTxt.text = "RED PLAYER WINS!";
		}
	}

	public void fillTotemsText(){
		Dictionary<string, int> totemsP1 = gameManager.getListNombreTotems (GameManager.LISTA_TOTEMS.LISTA_JUGADOR);
		Dictionary<string, int> totemsP2 = gameManager.getListNombreTotems (GameManager.LISTA_TOTEMS.LISTA_CONTRICANTE);

		totemsP1Txt.text = "RED PLAYER:\n";
		totemsP2Txt.text = "RED PLAYER:\n";

		foreach (KeyValuePair<string, int> entry in totemsP1) {
			if (entry.Value == 0) { //Muerto: pinta rojo
				totemsP1Txt.text += "<color=#fcb5ab>"+entry.Key.ToUpper()+"</color>\n"; 
			} else { // Vivo: pinta verde
				totemsP1Txt.text += "<color=#adffb8>"+entry.Key.ToUpper()+"</color>\n"; 
			}


		}

		foreach (KeyValuePair<string, int> entry in totemsP2) {
			if (entry.Value == 0) { //Muerto: pinta rojo
				totemsP2Txt.text += "<color=#fcb5ab>"+entry.Key.ToUpper()+"</color>\n"; 
			} else { // Vivo: pinta verde
				totemsP2Txt.text += "<color=#adffb8>"+entry.Key.ToUpper()+"</color>\n"; 
			}		}

	}
		


	// BUTTON LISTENERS
	public void PlayAgain(){
		SceneManager.LoadScene ("TeamsScene");

	}

	public void BackToMenu(){
		SceneManager.LoadScene ("MenuScene");
	}
}
