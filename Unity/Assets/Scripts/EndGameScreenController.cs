using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGameScreenController : MonoBehaviour {

	public Transform endGameCanvas; 
	//private StateHolder stateHolder;


	void Start() {
		//this.stateHolder = GetComponent<StateHolder>();
		endGameCanvas.gameObject.SetActive (false);
	}



	public void PlayAgain(){
		SceneManager.LoadScene ("JuegoScene");

	}

	public void BackToMenu(){
		SceneManager.LoadScene ("MenuScene");
	}
}
