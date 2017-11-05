using UnityEngine;
using System.Collections;

public class EndGameCondition : MonoBehaviour
{
    private StateHolder stateHolder;

    private GameManager gameManager;

	public Canvas canvasEndGame; 

	public EndGameScreenController egsc;



    // Use this for initialization
    void Start()
    {
		this.stateHolder = GetComponent<StateHolder>();
        this.gameManager = GetComponent<GameManager>();
        Debug.Log("EndGameCondition :: Start called");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.stateHolder.isPlaying() && this.IsWinCondition())
        {
            this.stateHolder.setMenu();
			showEndGameScreen ();

       }
    }

    // Si arribem al maxim de turns s'acaba el joc
    public bool IsWinCondition()
    {
        bool isWin = gameManager.isEmptyList(GameManager.LISTA_TOTEMS.LISTA_JUGADOR) || gameManager.isEmptyList(GameManager.LISTA_TOTEMS.LISTA_CONTRICANTE);
        //Debug.Log("EndGameCondition :: IsWinCondition called :: returns " + isWin.ToString() + ".");
        return isWin;
    }

    // Funció que tanca el joc quan es crida
    public void CloseGame()
    {
        Debug.Log("EndGameCondition :: CloseGame called :: closing the game.");
        
        Application.Quit();
    }

	// Funció que mostra la pantalla de fi de joc quan aquest finalitza
	private void showEndGameScreen()
	{
		canvasEndGame.gameObject.SetActive (true);
		egsc.fillWinnerText();
	}

}
