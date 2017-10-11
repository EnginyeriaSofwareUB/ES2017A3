using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    IDDLE,
    RIGHT,
    LEFT,
    JUMP,
    ATTACK
}

public class PlayerController : MonoBehaviour
{

    public float limiteRecorrido;

    public float recorrido;

    public static PlayerState currentPlayerState;

    public float lastX;

    bool GamePlaying = true; // esto luego sera un enum por cada estado del juego (pantalla inicial, pausa, jugando, final etc) que se extraera del controlador general del juego

    public GameObject player;
    
    // Use this for initialization
    void Start()
    {
        currentPlayerState = PlayerState.IDDLE;
        recorrido = 0;
        limiteRecorrido = 20f;
    }

    // Update is called once per frame
    void Update()
    {

        if (GamePlaying && doAction())
        {
            //Realizar el movimiento con su animacion y cambio de posicion o el ataque
            //Llamar a la funcion de MovimientoController
            //Despues de realizar el movimiento o ataque, se debe actualizar initialPosition y el estado del personaje a iddle
            player.SendMessage("Move");
            if (currentPlayerState == PlayerState.RIGHT || currentPlayerState == PlayerState.LEFT) recorrido += Math.Abs(player.transform.position.x - lastX);
            else if (currentPlayerState == PlayerState.ATTACK) recorrido = 0;
        }

    }

    bool doAction()
    {
        bool right = Input.GetKey("right");
        bool left = Input.GetKey("left");
        bool jump = Input.GetKey(KeyCode.Space);
        //bool shoot = Input.GetKey();

        bool doAction = false;

        if (jump)
        {
            currentPlayerState = PlayerState.JUMP;
            doAction = true;
        }
        else if(right || left)
        {
            doAction = checkTravel(right);
        }
        /*else if (shoot)
        {
            currentPlayerState = PlayerState.ATTACK;
            doAction = true;
        }*/

        return doAction;
    }

    bool checkTravel(bool right)
    {
        if (recorrido < limiteRecorrido)
        {
            lastX = player.transform.position.x;
            print("Recorrido:" + recorrido);
            print("Right:" + right);
            currentPlayerState = right ? PlayerState.RIGHT : PlayerState.LEFT;
            return true;
        }
        recorrido = limiteRecorrido;
        return false;
    }

}
