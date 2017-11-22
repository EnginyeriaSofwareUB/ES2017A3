using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    //public GameController gameController;

	public void StartGame(){
		SceneManager.LoadScene ("TeamsScene");
	}

	public void QuitGame(){
		Application.Quit();

	}
}
