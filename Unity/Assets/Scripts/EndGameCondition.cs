using UnityEngine;
using System.Collections;

public class EndGameCondition {

    private int turnCounter;
    private int maxTurns;

	private StateHolder stateHolder;


    public int MaxTurns
    {
        set
        {
            this.maxTurns = value;
        }

        get
        {
            return this.maxTurns;
        }
    }

    public EndGameCondition()
    {
        this.stateHolder = StateHolder.Instance;
        Debug.Log("EndGameCondition :: Start called");
        this.turnCounter = 0;
        this.MaxTurns = 4;
    }


	// Use this for initialization
	/*void Start ()
    {
        this.stateHolder = gameObject.GetComponent<StateHolder> ();
        print("EndGame");
        Debug.Log("EndGameCondition :: Start called");
        this.turnCounter = 0;
        this.MaxTurns = 4;
	}*/

	// Update is called once per frame
	/*void Update ()
    {
		if(this.stateHolder.isPlaying () && this.IsWinCondition ()){
			this.CloseGame ();
		}
	}*/

    // Si arribem al maxim de turns s'acaba el joc
     public bool IsWinCondition()
     {
        bool isWin = this.turnCounter >= this.MaxTurns;
        Debug.Log("EndGameCondition :: IsWinCondition called :: returns " + isWin.ToString() + ".");
        return isWin;
     }

    public void IncreaseTurnCounter()
    {
        this.turnCounter += 1;
        Debug.Log("EndGameCondition :: IncreaseTurnCounter called :: turnCounter = " + turnCounter.ToString() + ".");
    }

    // Funció que tanca el joc quan es crida
    public void CloseGame()
    {
        Debug.Log("EndGameCondition :: CloseGame called :: closing the game.");
        Application.Quit();
    }

}
