using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    List<GameObject> players;
    EndGameCondition gameCondition;
    StateHolder stateHolder;
    private int currentPlayer;
    public int MAX_PLAYERS = 2;

	// Use this for initialization
	void Start () {
        Debug.Log("GameController :: Start called");
       // stateHolder = StateHolder.Instance;
        stateHolder.setPlaying();
        gameCondition = new EndGameCondition();
        createPlayers();
    }
	
	// Update is called once per frame
	void Update () {

        if (stateHolder.isPlaying())
        {
            if (gameCondition.IsWinCondition())
            {
                gameCondition.CloseGame();
            }
            else
            {
                Debug.Log("Current player " + currentPlayer);
                bool changeTurn = getCurrentPlayer().Update();
                if (changeTurn)
                {
                    getCurrentPlayer().setChangeTurn(false);
                    gameCondition.IncreaseTurnCounter();
                    currentPlayer += 1;
                    currentPlayer = currentPlayer == MAX_PLAYERS ? 0 : currentPlayer; 
                }
            }
            //print("Update");

        }else if (stateHolder.isPause())
        {

        }
    }

    private PlayerController getCurrentPlayer()
    {
        return players[currentPlayer].GetComponent<PlayerController>();
    }

    private void createPlayers()
    {
        players = new List<GameObject>();
        for(int i=0;i < MAX_PLAYERS; i++)
        {
            GameObject playerInstance = Instantiate(Resources.Load("Player"), transform.position, Quaternion.identity) as GameObject;
            if (i == 0)
            {
                playerInstance.GetComponent<PlayerController>().Initialize(new Vector3(-7, 5, 0));
            }
            else
            {
                playerInstance.GetComponent<PlayerController>().Initialize(new Vector3(5, 5, 0));
            }
            
            players.Add(playerInstance);
        }
        
        currentPlayer = 0;
    }

}
