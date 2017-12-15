using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Environment;

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
    private int numCajasLanzadas;

    public Text txtNumeroRonda;
    public Text txtTurnoJugador;
    public Text txtRemainingSteps;

    private int ronda;
    
    private enum TURNO_JUGADOR { PRIMER_JUGADOR, SEGUNDO_JUGADOR }
    
    private enum PARTIDA_STATE { INICIO_RONDA, TURNO_RONDA, FIN_RONDA }

    public enum LISTA_TOTEMS { LISTA_JUGADOR, LISTA_CONTRICANTE }

    private TURNO_JUGADOR turnoJugador;
    private PARTIDA_STATE estadoPartida;
    public GameObject boxGenerator;

	private StateHolder stateHolder;


    private List<int> listaItemsPrimerJugador;
    private List<int> listaItemsSegundoJugador;

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

		gameObject.AddComponent<PlatformSpawner> ();

        //txtTurnoJugador.text = "Turno: " + turnoJugador.ToString();

        ronda = 0;

        turnCounter = 1;
        numCajasLanzadas = 1;

        this.listaItemsPrimerJugador = new List<int>();
        listaItemsPrimerJugador.Add(Global.TIPO_OBJETOS.objetoAngel);
        listaItemsPrimerJugador.Add(Global.TIPO_OBJETOS.objetoEscudoDoble);
        listaItemsPrimerJugador.Add(Global.TIPO_OBJETOS.objetoEscudoSimple);
        listaItemsPrimerJugador.Add(Global.TIPO_OBJETOS.objetoIglu);
        listaItemsPrimerJugador.Add(Global.TIPO_OBJETOS.objetoBomb);
        this.listaItemsSegundoJugador = new List<int>();

        listaItemsSegundoJugador.Add(Global.TIPO_OBJETOS.objetoAngel);
        listaItemsSegundoJugador.Add(Global.TIPO_OBJETOS.objetoEscudoDoble);
        listaItemsSegundoJugador.Add(Global.TIPO_OBJETOS.objetoEscudoSimple);
        listaItemsSegundoJugador.Add(Global.TIPO_OBJETOS.objetoIglu);
        listaItemsSegundoJugador.Add(Global.TIPO_OBJETOS.objetoBomb);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (turnoJugador== TURNO_JUGADOR.PRIMER_JUGADOR)
        {
            txtTurnoJugador.text = "BLUE TEAM";
            txtTurnoJugador.color = new Color(0f, 0f, 1f);
        }
        else
        {
            txtTurnoJugador.text = "RED TEAM";
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

        resetContornoTotemActual();

        // Por defecto, a cada inicio de ronda empezará el primer jugador con el movimiento activado
        this.totemActual = this.listaTotemsJugador.Poll();

        this.totemActual.activarControlMovimiento();

        // Finalmente,  actualizo el estado
        this.estadoPartida = PARTIDA_STATE.TURNO_RONDA;
        this.turnoJugador = TURNO_JUGADOR.PRIMER_JUGADOR;
        ronda += 1;
        txtNumeroRonda.text = "Round: " + ronda;
        StartCoroutine(intercambiarInventario());

        // Actualiza el contorno del módulo
        actualizarContornoTotemActual();
        if (BoxTurn()) ThrowBox();
    }

    /// <summary>
    /// Permite actualizar el contorno del totem actual a amarillo
    /// </summary>
    private void resetContornoTotemActual()
    {
        // Caso que se produce cuando el inicia el juego y aún no hay ningún totem 
        if (this.totemActual == null) return;

        // Obtengo todos los contornos de los módulos del totem
        ModuloTotem[] modulosTotem = this.totemActual.GetComponentsInChildren<ModuloTotem>();
        // Actualizo el color a amarillo
        foreach (ModuloTotem moduloTotem in modulosTotem)
            moduloTotem.resetColorContorno();
    }

    /// <summary>
    /// Permite actualizar el contorno del totem actual a amarillo
    /// </summary>
    private void actualizarContornoTotemActual()
    {
        if (this.totemActual == null) return;
        // Obtengo todos los contornos de los módulos del totem
        cakeslice.Outline[] outlineModulos = this.totemActual.GetComponentsInChildren<cakeslice.Outline>();
        // Actualizo el color a amarillo
        foreach (cakeslice.Outline outlineModulo in outlineModulos)
            outlineModulo.color = 2;
    }

    public void addTotemItems(Totem totemActual)
    {
        
        List<int> totemItems = totemActual.getItemList();
        foreach (int item in totemItems)
        {
            this.hotbar.addItemToInventory(item);
        }
        

    }

    public void AsignarItemTotem(int itemID)
    {
        if (!this.totemActual.HotbarLleno())
        {
            this.eliminarItemInventario(itemID);
            StartCoroutine(AñadirItemHotbar(itemID));
            totemActual.AddItem(itemID);
        }
        else
        {
            StartCoroutine(AñadirItemInventario(itemID));
        }
        
    }

    public IEnumerator AñadirItemHotbar(int itemID)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        this.hotbar.addItemToInventory(itemID);
    }

    public IEnumerator AñadirItemInventario(int itemID)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        this.inventario.addItemToInventory(itemID);
    }

    private void handleTurno()
    {
        // En caso que el totem del jugador actual no exceda la distancia desactivo su movimiento
        while (!totemActual.excedeLimiteDistancia()){
            actualizarDistancia();
            return;
        }

        Debug.Log("Intercambio turno");

        if (turnoJugador == TURNO_JUGADOR.SEGUNDO_JUGADOR)
            estadoPartida = PARTIDA_STATE.FIN_RONDA;
        else
            intercambiarTurno();

        turnCounter += 1;
    }

  
    private void handleFinalRonda()
    {
        //Si han pasado X turnos (box turn) y no ha sucedido la condición de final, lanzamos la caja a la escena

        txtNumeroRonda.text = "Round: " + ronda;

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
        if(totemActual != null)
        {
            this.totemActual.desabilitarControlMovimiento();
            ModuloTotem modulo = this.totemActual.GetComponentInChildren<ModuloTotem>();
            modulo.resetColorContorno();

        }

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
        actualizarContornoTotemActual();
        StartCoroutine(intercambiarInventario());
        if (!BoxTurn()) ThrowBox();
        SwitchBoxTurn();

    }

    public void actualizarDistancia()
    {
        this.txtRemainingSteps.text = totemActual.distanciaRestante();
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

    private bool BoxTurn()
    {
        //return turnCounter % GetNumBoxTurn() == 0;
        return !condicionFinJuego.IsWinCondition() && boxGenerator.GetComponent<BoxGeneratorController>().boxTurn;
    }

    private void SwitchBoxTurn()
    {
        boxGenerator.GetComponent<BoxGeneratorController>().boxTurn = !boxGenerator.GetComponent<BoxGeneratorController>().boxTurn;
    }

    private void ThrowBox()
    {
        Debug.Log("ThrowBox");
        for(int i=0; i < this.numCajasLanzadas; i++)
        {
            boxGenerator.GetComponent<BoxGeneratorController>().SendMessage("AddBox");
        }
       
    }

	private void initPlayers()
	{
        this.stateHolder.setPlaying();
        Time.timeScale = 1;
		listaTotemsJugador = new PriorityQueue<Totem>();
		listaTotemsContrincante = new PriorityQueue<Totem>();
		listaNombreTotemsJugador = new Dictionary<string, int>();
		listaNombreTotemsContrincante = new Dictionary<string, int>();
        GameObject firstPlayerTotems = GameObject.FindGameObjectWithTag("FirstPlayer");
        GameObject secondPlayerTotems = GameObject.FindGameObjectWithTag("SecondPlayer");
       
        /* 
            // Creación de un totem dinamicamente               Posicion dentro de la escena
            GameObject totem = Instantiate(this.totem, new Vector3(-11.99f, 12.42f, 0.1011065f), Quaternion.identity);
            // Cambio de tag
            totem.tag =  Global.TOTEM_SECOND_PLAYER;
            // Nombre para encontrarlo
            totem.name ="Itsme";
            // Layer 9 = TotemsSegundoJugador, Layer 8 = TotemsPrimerJugador
            totem.layer = 9;
            // Assignar el totem al player correspondiente
            totem.transform.parent = GameObject.FindGameObjectWithTag("SecondPlayer").transform;
        */
        for (int j = 0; j < TeamsData.PlayersRed; j++)
		{
            Vector3 position = new Vector3(UnityEngine.Random.Range(-15, 30), 2f, 0.11f);
            GameObject totemPlayer = Instantiate(Resources.Load("TotemPlayer"), position, Quaternion.identity) as GameObject;
            totemPlayer.tag = Global.TOTEM_FIRST_PLAYER;
            totemPlayer.layer = 8;
            totemPlayer.name = "TotemRed" + j;
            totemPlayer.GetComponent<Totem>().CreateTotem();
            totemPlayer.transform.parent = firstPlayerTotems.transform;
				//Add modules to totem:

				for(int i = 0; i<4; i++){
					int modul = getModuleTotem (1, j, i);
					switch(modul){
						case 1:
                         totemPlayer.GetComponent<Totem> ().AddAguilaTotem ();
							break;
						case 2:
                            totemPlayer.GetComponent<Totem> ().AddGorilaTotem ();
							break;
						case 3:
                            totemPlayer.GetComponent<Totem> ().AddElefanteTotem();
							break;
						case 4:
                            totemPlayer.GetComponent<Totem> ().AddTortugaTotem();
							break;
						default:

							break;
						}
                }

            listaTotemsJugador.Add(totemPlayer.GetComponent<Totem>());
            listaNombreTotemsJugador.Add(totemPlayer.GetComponent<Totem>().name, 1);

		}

        for (int j = 0; j < TeamsData.PlayersBlue; j++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-15, 30), 2f, 0.11f);
            GameObject totemPlayer = Instantiate(Resources.Load("TotemPlayer"), position, Quaternion.identity) as GameObject;
            totemPlayer.tag = Global.TOTEM_SECOND_PLAYER;
            totemPlayer.layer = 9;
            totemPlayer.name = "TotemBlue" + j;
            totemPlayer.GetComponent<Totem>().CreateTotem();
            totemPlayer.transform.parent = secondPlayerTotems.transform;


            //Add modules to totem:
            for (int i = 0; i<4; i++){
				int modul = getModuleTotem(2, j, i);
				if (modul!= 0) {
					switch(modul){
					case 1:
                       totemPlayer.GetComponent<Totem> ().AddAguilaTotem ();
						break;
					case 2:
                       totemPlayer.GetComponent<Totem> ().AddGorilaTotem ();
						break;
					case 3:
                        totemPlayer.GetComponent<Totem> ().AddElefanteTotem();
						break;
					case 4:
                        totemPlayer.GetComponent<Totem> ().AddTortugaTotem();
						break;
					}
                }

            }


            listaTotemsContrincante.Add (totemPlayer.GetComponent<Totem> ());
			listaNombreTotemsContrincante.Add (totemPlayer.GetComponent<Totem> ().name, 1);
			
		}
	}

    public bool isEmptyList(LISTA_TOTEMS lista){
        PriorityQueue<Totem> listToCheck = null;
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
        if (listToCheck != null)
            return listToCheck.isEmpty();
        else
            return true;
    }

    public void RemoveTotem(Totem totem)
    {
        if (totem.tag == Global.TOTEM_FIRST_PLAYER)
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


    public void guardarItem(Totem totem,int itemID)
    {
		if (totem.tag == totemActual.tag) {
			this.inventario.addItemToInventory (itemID);
		} 
			
		if (totem.tag == Global.TOTEM_FIRST_PLAYER)
            this.listaItemsPrimerJugador.Add(itemID);
        else
            this.listaItemsSegundoJugador.Add(itemID);
    }

    public void eliminarItemInventario(int itemID)
    {
        if (turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR)
            this.listaItemsPrimerJugador.Remove(itemID);
        else
            this.listaItemsSegundoJugador.Remove(itemID);
        
    }

    public void eliminarItemHotbar(int itemID)
    {
        totemActual.EliminarItem(itemID);
    }

    IEnumerator intercambiarInventario()
    {
        this.inventario.deleteAllItems();
        this.hotbar.deleteAllItems();
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        if (turnoJugador == TURNO_JUGADOR.PRIMER_JUGADOR)
        {
            foreach (int item in this.listaItemsPrimerJugador)
            {
                //En caso de stackear el item con el número de items cogidos, se pasara el itemValue y el array será de Item envez de int
                this.inventario.addItemToInventory(item);
            }
        }
        else
        {
            foreach (int item in this.listaItemsSegundoJugador)
            {
                //En caso de stackear el item con el número de items cogidos, se pasara el itemValue y el array será de Item envez de int
                this.inventario.addItemToInventory(item);
            }
        }
        this.inventario.updateItemList();
        this.inventario.stackableSettings();
        this.addTotemItems(this.totemActual);
    }

    public int GetRondaActual()
    {
        return ronda;
    }

}
