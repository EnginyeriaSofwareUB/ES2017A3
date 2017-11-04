using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Lista de totems del contrincante. Se pueden asignar desde el editor de unity
    private PriorityQueue<Totem> listaTotemsContrincante;
    // Lista de totems del jugador. Se pueden asignar desde el editor de unity
    private PriorityQueue<Totem> listaTotemsJugador;

    // Componente de GameManager que indica cuando se acaba la partida
    private EndGameCondition condicionFinJuego;
    // Totem actual del jugadorIsWinCondition
    public Totem totemActual;

    private int turnCounter;

    public Text txtNumeroRonda;
    public Text txtTurnoJugador;

    private int ronda;
    
    private enum TURNO_JUGADOR { PRIMER_JUGADOR, SEGUNDO_JUGADOR }
    
    private enum PARTIDA_STATE { INICIO_RONDA, TURNO_RONDA, FIN_RONDA }

    public enum LISTA_TOTEMS { LISTA_JUGADOR, LISTA_CONTRICANTE }

    private TURNO_JUGADOR turnoJugador;
    private PARTIDA_STATE estadoPartida;
    public GameObject boxGenerator;

	private StateHolder stateHolder;

    private void Awake()
    {


    }
    // Use this for initialization
    void Start()
    {
		this.stateHolder = GetComponent<StateHolder>();
		initPlayers();
        estadoPartida = PARTIDA_STATE.INICIO_RONDA;
        turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;

        condicionFinJuego = GetComponent<EndGameCondition>();

        txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();

        ronda = 0;

        turnCounter = 1;

    }


    // Update is called once per frame
    void LateUpdate()
    {
        switch (estadoPartida)
        {
            case PARTIDA_STATE.INICIO_RONDA:
                handleInicioRonda();
                break;

            case PARTIDA_STATE.TURNO_RONDA:
                handleTurno();
                break;

            case PARTIDA_STATE.FIN_RONDA:
                handleFinalRonda();
                break;
        }

    }



    private void handleInicioRonda()
    {
        // Primeramente desactivo el movimiento de todos los totems
        this.desactivarMovimientoTotems();


        // Por defecto, a cada inicio de ronda empezará el primer jugador con el movimiento activado
        this.totemActual = this.listaTotemsJugador.Poll();
        this.totemActual.activarControlMovimiento();


        // Finalmente,  actualizo el estado
        this.estadoPartida = PARTIDA_STATE.TURNO_RONDA;
        this.turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;
        ronda += 1;
        txtNumeroRonda.text = "Ronda: " + ronda;

    }


    private void handleTurno()
    {
        // En caso que el totem del jugador actual no exceda la distancia desactivo su movimiento
        while (!totemActual.excedeLimiteDistancia()){
            return;
        }

        Debug.Log("Intercambio turno");

        if (turnoJugador == TURNO_JUGADOR.SEGUNDO_JUGADOR)
            estadoPartida = PARTIDA_STATE.FIN_RONDA;
        else
            intercambiarTurno();
        turnCounter += 1;
        if (BoxTurn() && !condicionFinJuego.IsWinCondition()) ThrowBox();
    }

    private void handleFinalRonda()
    {
        //Si han pasado X turnos (box turn) y no ha sucedido la condición de final, lanzamos la caja a la escena

        txtNumeroRonda.text = "Ronda: " + ronda;

        estadoPartida = PARTIDA_STATE.INICIO_RONDA;
        Debug.Log("Fin de la ronda");


    }
    /// <summary>
    /// Función que permite intecambiar el turno de un jugador.
    /// Primeramente, desactiva el movimiento de todos los totems de los dos jugadores.
    /// </summary>
    private void intercambiarTurno()
    {
        // Desactivo el movimiento del totem del jugador
        this.totemActual.desabilitarControlMovimiento();
        //this.condicionFinJuego.IncreaseTurnCounter();

        // Intercambio el turno del jugador
        turnoJugador = turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR ? TURNO_JUGADOR.SEGUNDO_JUGADOR : TURNO_JUGADOR.PRIMER_JUGADOR;

        switch (turnoJugador)
        {
            case TURNO_JUGADOR.PRIMER_JUGADOR:
                totemActual = this.listaTotemsJugador.Poll();
                totemActual.activarControlMovimiento();
                break;
            case TURNO_JUGADOR.SEGUNDO_JUGADOR:
                totemActual = this.listaTotemsContrincante.Poll();
                totemActual.activarControlMovimiento();
                break;
        }

        // Muestro el turno del jugador
        txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();
       

    }

    /// <summary>
    /// Método que permite desactivar el movimiento de todos los totems del primer jugador
    /// </summary>
    private void desactivarMovimientoTotemJugador()
    {
        foreach (Totem value in listaTotemsJugador)
        {
            value.desabilitarControlMovimiento();
        }
    }

    /// <summary>
    /// Método que permite desactivar el movimiento de todos los totems del segundo jugador
    /// </summary>
    private void desactivarMovimientoTotemContrincante()
    {
        foreach (Totem value in listaTotemsContrincante)
        {
            value.desabilitarControlMovimiento();
        }
    }

    /// <summary>
    /// Función que permite desactivar el movimiento de todos los totems de todos los jugadores
    /// Este metodo se llama al inicio de la ronda
    /// </summary>
    private void desactivarMovimientoTotems()
    {
        foreach (Totem value in listaTotemsJugador)
        {
            value.desabilitarControlMovimiento();
        }
        foreach (Totem value in listaTotemsContrincante)
        {
            value.desabilitarControlMovimiento();
        }
    }

    private int GetNumBoxTurn()
    {
        return boxGenerator.GetComponent<BoxGeneratorController>().boxTurn;
    }

    private bool BoxTurn()
    {
        return (turnCounter-1) % GetNumBoxTurn() == 0;
    }

    private void ThrowBox()
    {
        Debug.Log("ThrowBox");
        boxGenerator.GetComponent<BoxGeneratorController>().SendMessage("AddBox");
    }

    private void initPlayers()
    {
        listaTotemsJugador = new PriorityQueue<Totem>();
        listaTotemsContrincante = new PriorityQueue<Totem>();
        Object[] allFirstPlayerTotems = GameObject.FindGameObjectsWithTag("FirstPlayer");
        Object[] allSecondPlayerTotems = GameObject.FindGameObjectsWithTag("SecondPlayer");

        foreach(GameObject firstPlayerTotem in allFirstPlayerTotems)
        {
            listaTotemsJugador.Add(firstPlayerTotem.GetComponent<Totem>());
        }

        foreach (GameObject secondPlayerTotem in allSecondPlayerTotems)
        {
            listaTotemsContrincante.Add(secondPlayerTotem.GetComponent<Totem>());
        }
    }

    public bool isEmptyList(LISTA_TOTEMS lista){
        PriorityQueue<Totem> listToCheck;
        switch(lista){
            case(LISTA_TOTEMS.LISTA_JUGADOR):
                listToCheck = listaTotemsJugador;
                break;
            case(LISTA_TOTEMS.LISTA_CONTRICANTE):
                listToCheck = listaTotemsContrincante;
                break;
            default:
                return false;
        }
        return listToCheck.isEmpty();
    }

    public void RemoveTotem(Totem totem)
    {
        if (totem.tag == "FirstPlayer")
        {
            listaTotemsJugador.Remove(totem);
        }
        else
        {
            listaTotemsContrincante.Remove(totem);
        }
        Destroy(totem.gameObject);
    }

}
