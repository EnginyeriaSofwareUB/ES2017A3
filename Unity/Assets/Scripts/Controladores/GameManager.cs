using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }
    
    public GameObject totem;

    // Lista de totems del contrincante. Se pueden asignar desde el editor de unity
    private PriorityQueue<Totem> listaTotemsContrincante;
	//Diccionario con los nombres de los totems del contrincante y valor 1 si esta vivo, 0 si muere
	private Dictionary<string, int> listaNombreTotemsContrincante;
    // Lista de totems del jugador. Se pueden asignar desde el editor de unity
    private PriorityQueue<Totem> listaTotemsJugador;
	//Diccionario con los nombres de los totems del jugador y valor 1 si esta vivo, 0 si muere
	private Dictionary<string, int> listaNombreTotemsJugador;

    // Componente de GameManager que indica cuando se acaba la partida
    private EndGameCondition condicionFinJuego;
    // Totem actual del jugadorIsWinCondition
    public Totem totemActual;

    private GestionInventario gestorInventario;

    private GestionHotbar gestorHotbar;

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


    private List<Item> listaItemsPrimerJugador;
    private List<Item> listaItemsSegundoJugador;

    private Inventory inventario;

    private Inventory hotbar;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Use this for initialization
    void Start()
    {
		this.stateHolder = GetComponent<StateHolder>();
		initPlayers();
        estadoPartida = PARTIDA_STATE.INICIO_RONDA;
        turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;

        GameObject inventarioGameObject= GameObject.FindGameObjectWithTag("MainInventory");
        this.inventario = inventarioGameObject.GetComponent<Inventory>();
        GameObject hotbarGameObject = GameObject.FindGameObjectWithTag("Hotbar");
        this.hotbar = hotbarGameObject.GetComponent<Inventory>();
        gestorInventario = this.gameObject.AddComponent<GestionInventario>();
        gestorInventario.inventory = inventarioGameObject;
        gestorHotbar = this.gameObject.AddComponent<GestionHotbar>();
        gestorHotbar.inventory = hotbarGameObject; 
        condicionFinJuego = GetComponent<EndGameCondition>();

        //txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();

        ronda = 0;

        turnCounter = 1;

        this.listaItemsPrimerJugador = new List<Item>();
        this.listaItemsSegundoJugador = new List<Item>();
        
        
        inicializarContorno();
    }


    private void inicializarContorno()
    {
        
        GameObject[] totemsPrimerEquipo = GameObject.FindGameObjectsWithTag("TotemFirstPlayerModule");
        GameObject[] totemsSegundoEquipo = GameObject.FindGameObjectsWithTag("TotemSecondPlayerModule");
        foreach (GameObject totem in totemsPrimerEquipo)
        {
            if(totem!=null)
            totem.transform.GetChild(0).gameObject.AddComponent<cakeslice.Outline>();
        }
        foreach (GameObject totem in totemsSegundoEquipo)
        {
            if(totem!=null)
            totem.transform.GetChild(0).gameObject.AddComponent<cakeslice.Outline>();
        }
        actualizarContornos();
    }
    private void actualizarContornos()
    {
        GameObject[] totemsPrimerEquipo = GameObject.FindGameObjectsWithTag("TotemFirstPlayerModule");
        GameObject[] totemsSegundoEquipo = GameObject.FindGameObjectsWithTag("TotemSecondPlayerModule");
        foreach (GameObject totem in totemsPrimerEquipo)
        {
            if (totem == null)
            {
                Debug.Log("sdad");
            }else
                totem.transform.GetChild(0).GetComponent<cakeslice.Outline>().color = 0;
        }
        foreach (GameObject totem in totemsSegundoEquipo)
        {
            if (totem == null)
            {
                Debug.Log("sdad");
            }
            else
            {
                totem.transform.GetChild(0).GetComponent<cakeslice.Outline>().color = 1;
            }
        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (turnoJugador== TURNO_JUGADOR.PRIMER_JUGADOR)
        {
            txtTurnoJugador.text = "Es tu turno";
            txtTurnoJugador.color = new Color(0f, 1f, 0f);
        }
        else
        {
            txtTurnoJugador.text = "Turno del contrincante";
            txtTurnoJugador.color = new Color(1f, 0f, 0f);

        }
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
        this.addTotemItems(this.totemActual);


        // Finalmente,  actualizo el estado
        this.estadoPartida = PARTIDA_STATE.TURNO_RONDA;
        this.turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;
        ronda += 1;
        txtNumeroRonda.text = "Ronda: " + ronda;
       // InitInventory();

    }

    private void addTotemItems(Totem totemActual)
    {
        this.hotbar.deleteAllItems();
        foreach (Item item in totemActual.getItemList()){
            this.hotbar.addItemToInventory(item.itemID);
        }
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
        //if (BoxTurn() && !condicionFinJuego.IsWinCondition()) ThrowBox();
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
    public void intercambiarTurno()
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
        intercambiarInventario();
       // InitInventory();
        // Muestro el turno del jugador
        //txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();


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
		int countRed = 0;
		int countBlue = 0;

		listaTotemsJugador = new PriorityQueue<Totem>();
		listaTotemsContrincante = new PriorityQueue<Totem>();
		listaNombreTotemsJugador = new Dictionary<string, int>();
		listaNombreTotemsContrincante = new Dictionary<string, int>();
		UnityEngine.Object[] allFirstPlayerTotems = GameObject.FindGameObjectsWithTag("TotemFirstPlayer");
		UnityEngine.Object[] allSecondPlayerTotems = GameObject.FindGameObjectsWithTag("TotemSecondPlayer");
    /* 
        // Creación de un totem dinamicamente               Posicion dentro de la escena
        GameObject totem = Instantiate(this.totem, new Vector3(-11.99f, 12.42f, 0.1011065f), Quaternion.identity);
        // Cambio de tag
        totem.tag = "TotemSecondPlayer";
        // Nombre para encontrarlo
        totem.name ="Itsme";
        // Layer 9 = TotemsSegundoJugador, Layer 8 = TotemsPrimerJugador
        totem.layer = 9;
        // Assignar el totem al player correspondiente
        totem.transform.parent = GameObject.FindGameObjectWithTag("SecondPlayer").transform;
    */
		foreach(GameObject firstPlayerTotem in allFirstPlayerTotems)
		{
			if (countRed < TeamsData.PlayersRed) 
			{
				//Add modules to totem:
				for(int i = 0; i<4; i++){
					int modul = getModuleTotem (1, countRed, i);
					switch(modul){
						case 1:
							firstPlayerTotem.GetComponent<Totem> ().AddAguilaTotem ();
							break;
						case 2:
							firstPlayerTotem.GetComponent<Totem> ().AddGorilaTotem ();
							break;
						case 3:
							firstPlayerTotem.GetComponent<Totem> ().AddElefanteTotem();
							break;
						case 4:
							firstPlayerTotem.GetComponent<Totem> ().AddTortugaTotem();
							break;
						default:

							break;
						}


                }

                listaTotemsJugador.Add (firstPlayerTotem.GetComponent<Totem> ());
				listaNombreTotemsJugador.Add (firstPlayerTotem.GetComponent<Totem> ().name, 1);
			} else {
				firstPlayerTotem.gameObject.SetActive (false);
			}

			countRed++;

		}

		foreach (GameObject secondPlayerTotem in allSecondPlayerTotems)
		{
			if (countBlue < TeamsData.PlayersBlue) {

				//Add modules to totem:
				for(int i = 0; i<4; i++){
					int modul = getModuleTotem(2, countBlue, i);
					if (modul!= 0) {
						switch(modul){
						case 1:
							secondPlayerTotem.GetComponent<Totem> ().AddAguilaTotem ();
							break;
						case 2:
							secondPlayerTotem.GetComponent<Totem> ().AddGorilaTotem ();
							break;
						case 3:
							secondPlayerTotem.GetComponent<Totem> ().AddElefanteTotem();
							break;
						case 4:
							secondPlayerTotem.GetComponent<Totem> ().AddTortugaTotem();
							break;
						}
                    }

                }


                listaTotemsContrincante.Add (secondPlayerTotem.GetComponent<Totem> ());
				listaNombreTotemsContrincante.Add (secondPlayerTotem.GetComponent<Totem> ().name, 1);
			} else {
				secondPlayerTotem.gameObject.SetActive (false);
			}

			countBlue++;

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
			listaNombreTotemsJugador [totem.name] = 0;
        }
        else
        {
            listaTotemsContrincante.Remove(totem);
			listaNombreTotemsContrincante [totem.name] = 0;

        }
        Destroy(totem.gameObject);
    }


	public Dictionary<string, int> getListNombreTotems(LISTA_TOTEMS lista)
	{
		switch(lista) {
			case(LISTA_TOTEMS.LISTA_JUGADOR):
				 return listaNombreTotemsJugador;
			case(LISTA_TOTEMS.LISTA_CONTRICANTE):
				return listaNombreTotemsContrincante;
			default:
				return null;
		}
	}


	private int getModuleTotem(int player, int currentTotem, int posModul){
		//TeamsData.ModulesTotem1P1 [i];
		int ret = 0;
		currentTotem++;
		if (player == 1) 
		{
			//Debug.Log ("in player 1"+ret+"ct"+currentTotem);
			switch (currentTotem) 
			{
			case 1: 
				ret = TeamsData.ModulesTotem1P1 [posModul];
				break;
			case 2: 
				ret = TeamsData.ModulesTotem2P1 [posModul];			
				break;
			case 3: 
				ret = TeamsData.ModulesTotem3P1 [posModul];
				break;
			case 4:
				ret = TeamsData.ModulesTotem4P1 [posModul];
				break;
			case 5: 
				ret = TeamsData.ModulesTotem5P1 [posModul];
				break;

			}

		} else 
		{
			//Debug.Log ("in player 2"+ret+"ct"+currentTotem);
			switch (currentTotem) 
			{
			case 1: 
				ret = TeamsData.ModulesTotem1P2 [posModul];
				break;
			case 2: 
				ret = TeamsData.ModulesTotem2P2 [posModul];			
				break;
			case 3: 
				ret = TeamsData.ModulesTotem3P2 [posModul];
				break;
			case 4:
				ret = TeamsData.ModulesTotem4P2 [posModul];
				break;
			case 5: 
				ret = TeamsData.ModulesTotem5P2 [posModul];
				break;

			}

		}

		return ret;
	}

    public Totem getActualTotem()
    {
        return totemActual;
    }


    public void guardarItem(Item item)
    {
        if (turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR)
            this.listaItemsPrimerJugador.Add(item);
        else
            this.listaItemsSegundoJugador.Add(item);
    }

    public void eliminarItem(Item item)
    {
        if (turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR)
            this.listaItemsPrimerJugador.Remove(item);
        else
            this.listaItemsSegundoJugador.Remove(item);
    }

    private void intercambiarInventario()
    {

        if (turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR)
        {
            foreach (Item item in this.listaItemsPrimerJugador)
            {
                this.inventario.addItemToInventory(item.itemID, item.itemValue);
            }
        }
        else
        {
            foreach (Item item in this.listaItemsSegundoJugador)
            {
                this.inventario.addItemToInventory(item.itemID, item.itemValue);
            }
        }
        this.inventario.updateItemList();
        this.inventario.stackableSettings();
    }

}
