using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

	public Transform pauseCanvas; 


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
		Time.timeScale = 0;
	}

	private void closePopup(){
		pauseCanvas.gameObject.SetActive (false);
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
