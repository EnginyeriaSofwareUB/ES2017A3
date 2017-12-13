using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

	public Transform pauseCanvas;
	public Transform endGameCanvas;
	public Canvas canvas;

	private StateHolder stateHolder;
	

	void Start() {
		this.stateHolder = GetComponent<StateHolder>();
		pauseCanvas.gameObject.SetActive (false);
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !endGameCanvas.gameObject.activeInHierarchy){

			if (pauseCanvas.gameObject.activeInHierarchy) {
				closePopup ();
			} else {
				openPopup ();
			}

		}
	}

	private void openPopup(){
		canvas.gameObject.SetActive (false);
		pauseCanvas.gameObject.SetActive (true);
		stateHolder.setPause ();
		Time.timeScale = 0;
	}

	private void closePopup(){
		canvas.gameObject.SetActive (true);
		pauseCanvas.gameObject.SetActive (false);
		if(GetComponent<GestionInventario>().isInventoryOpen())
			stateHolder.setInventary();
		else
			stateHolder.setPlaying ();
		Time.timeScale = 1;
	}


	// Buttons on click

	public void ContinueGame(){
		closePopup ();
	}

	public void ExitGame(){
		SceneManager.LoadScene ("MenuScene");
	}
}
