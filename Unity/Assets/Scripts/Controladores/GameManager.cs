using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    // Lista de totems del contrincante. Se pueden asignar desde el editor de unity
    [SerializeField] private List<Totem> listaTotemsContrincante;
    // Lista de totems del jugador. Se pueden asignar desde el editor de unity
    [SerializeField] private List<Totem> listaTotemsJugador;

    // Componente de GameManager que indica cuando se acaba la partida
    private EndGameCondition condicionFinJuego;
    // Totem actual del jugador
    private Totem totemActual;

    public Text txtNumeroRonda;
    public Text txtTurnoJugador;

    
    private enum TURNO_JUGADOR { PRIMER_JUGADOR, SEGUNDO_JUGADOR }
    
    private enum PARTIDA_STATE { INICIO_RONDA, TURNO_RONDA, FIN_RONDA }

    private TURNO_JUGADOR turnoJugador;
    private PARTIDA_STATE estadoPartida;


    private void Awake()
    {


    }
    // Use this for initialization
    void Start()
    {
        estadoPartida = PARTIDA_STATE.INICIO_RONDA;
        turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;

        condicionFinJuego = GetComponent<EndGameCondition>();

        txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();

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
        this.totemActual = this.listaTotemsJugador[0];
        this.totemActual.activarControlMovimiento();


        // Finalmente,  actualizo el estado
        this.estadoPartida = PARTIDA_STATE.TURNO_RONDA;
        this.turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;
        txtNumeroRonda.text = "Ronda: " + condicionFinJuego.TurnCounter;

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


    }

    private void handleFinalRonda()
    {
        condicionFinJuego.IncreaseTurnCounter();
        txtNumeroRonda.text = "Ronda: " + condicionFinJuego.TurnCounter;

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
        this.condicionFinJuego.IncreaseTurnCounter();

        // Intercambio el turno del jugador
        turnoJugador = turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR ? TURNO_JUGADOR.SEGUNDO_JUGADOR : TURNO_JUGADOR.PRIMER_JUGADOR;

        switch (turnoJugador)
        {
            case TURNO_JUGADOR.PRIMER_JUGADOR:
                totemActual = this.listaTotemsJugador[0];
                totemActual.activarControlMovimiento();
                break;
            case TURNO_JUGADOR.SEGUNDO_JUGADOR:
                totemActual = this.listaTotemsContrincante[0];
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

}
