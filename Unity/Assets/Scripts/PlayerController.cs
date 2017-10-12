using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentTotemState{
    IDDLE,
    RIGHT,
    LEFT,
    JUMP,
    ATTACK
}

public class PlayerController: MonoBehaviour
{

    public float maxTravel; //Esto es comun a ambos players y podria estar en game controller

    public float currentTotemTravel;

    public static CurrentTotemState currentTotemState;

    public float lastX;

    private bool changeTurn;

    private List<GameObject> playerTotems;

    private int currentTotem;

    public int MAX_TOTEMS = 1; //Esto es comun a ambos players y podria estar en game controller

    private Vector3 totemPosition;

    public MovimientoController movementController;

    /*public PlayerController()
    {
        currentTotemState = CurrentTotemState.IDDLE;
        currentTotemTravel = 0;
        maxTravel = 20f;
        changeTurn = false;
        createTotems();
    }*/
    public void Initialize(Vector3 position)
    {
        totemPosition = position;
        transform.position = position;
    }
    void Start()
    {
        Debug.Log("PlayerController :: Start called");
        currentTotemState = CurrentTotemState.IDDLE;
        currentTotemTravel = 0;
        maxTravel = 20f;
        changeTurn = false;
        CreateTotems();
    }

    public void setChangeTurn(bool change)
    {
        this.changeTurn = change;
    }

    // Update is called once per frame
    public bool Update()
    {
        if (doAction())
        {
            //Realizar el movimiento con su animacion y cambio de posicion o el ataque
            //Llamar a la funcion de MovimientoController
            //Despues de realizar el movimiento o ataque, se debe actualizar initialPosition y el estado del personaje a iddle

            if(currentTotemState == CurrentTotemState.ATTACK)
            {
                //movementController.SendMessage("Attack", GetCurrentTotem()); //Falta el gameobject que permite atacar
                currentTotemTravel = 0;
                changeTurn = true;
            }
            else
            {
                movementController.Move(GetCurrentTotem());
                if (currentTotemState == CurrentTotemState.RIGHT || currentTotemState == CurrentTotemState.LEFT) currentTotemTravel += Math.Abs(GetCurrentTotem().transform.position.x - lastX);
            }
            currentTotemState = CurrentTotemState.IDDLE;
           
        }

        return changeTurn;

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

            currentTotemState = CurrentTotemState.JUMP;
            //checkear si esta en el aire no hacer jump
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
        if (currentTotemTravel < maxTravel)
        {
            lastX = GetCurrentTotem().transform.position.x;
            //Console("Recorrido:" + recorrido);
            //print("Right:" + right);
            currentTotemState = right ? CurrentTotemState.RIGHT : CurrentTotemState.LEFT;
            return true;
        }
        changeTurn = true; //Una vez se haya creado el ataque esta linea sobrará, ya que se cambiara de turno una vez ataque
        currentTotemTravel = 0; //Esta igual que la de arriba, (recorrido = limiteRecorrido)
        return false;
    }

    private Totem GetCurrentTotem()
    {
        return playerTotems[currentTotem].GetComponent<Totem>();
    }

    private void CreateTotems()
    {
        movementController = new MovimientoController();
        playerTotems = new List<GameObject>();
        for (int i = 0; i < MAX_TOTEMS; i++)
        {
            GameObject totemInstance = Instantiate(Resources.Load("TotemController"), totemPosition,Quaternion.identity) as GameObject;
            //Totem totem = new Totem();
            //GameObject totemInstance = Instantiate(totem.gameObject, totemPosition, Quaternion.identity) as GameObject;
            playerTotems.Add(totemInstance);
        }

        currentTotem = 0;
        
    }

}
