using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement{
    IDDLE,
    RIGHT,
    LEFT,
    JUMP,
    ATTACK
}

public class PlayerController : MonoBehaviour
{

    public int limiteRecorrido;

    public int pasos;

    public Movement currentPlayerState;

    public Vector3 initialPosition;

    bool GamePlaying = true; // esto luego sera un enum por cada estado del juego (pantalla inicial, pausa, jugando, final etc) que se extraera del controlador general del juego

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
        currentPlayerState = Movement.IDDLE;
        pasos = 0;
        limiteRecorrido = 20;
    }

    // Update is called once per frame
    void Update()
    {

        if (GamePlaying && doAction())
        {
            //Realizar el movimiento con su animacion y cambio de posicion o el ataque
            //Llamar a la funcion de MovimientoController
            //Despues de realizar el movimiento o ataque, se debe actualizar initialPosition y el estado del personaje a iddle
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
            currentPlayerState = Movement.JUMP;
            doAction = true;
        }
        else if(right || left)
        {
            doAction = checkTravel(right);
        }
        /*else if (shoot)
        {
            currentPlayerState = Movement.ATTACK;
            doAction = true;
        }*/

        return doAction;
    }

    bool checkTravel(bool right)
    {
        float recorrido = Vector3.Magnitude(transform.position - initialPosition) * Time.deltaTime;
        if (recorrido < limiteRecorrido)
        {
            print("Recorrido:" + recorrido);
            currentPlayerState = right ? Movement.RIGHT : Movement.LEFT;
            return true;
        }
        return false;
    }

}
