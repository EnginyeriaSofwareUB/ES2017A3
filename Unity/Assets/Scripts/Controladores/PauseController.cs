using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

	public Transform pauseCanvas; 
	private StateHolder stateHolder;


	void Start() {
		this.stateHolder = GetComponent<StateHolder>();
	}


	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){

			if (pauseCanvas.gameObject.activeInHierarchy) {
				closePopup ();
			} else {
				openPopup ();
			}

		}
	}

	private void openPopup(){
		pauseCanvas.gameObject.SetActive (true);
		stateHolder.setPause ();
		Time.timeScale = 0;
	}

	private void closePopup(){
		pauseCanvas.gameObject.SetActive (false);
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
