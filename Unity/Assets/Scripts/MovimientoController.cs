
using UnityEngine;


public class MovimientoController : MonoBehaviour
{
    enum ESTADO_SALTO
    {
        EN_TIERRA,
        SALTANDO,
        CAYENDO
    }
    // Velocidad de desplazamiento del jugador
    [SerializeField] private float velocidadMovimiento = 10f;

    // Velocidad de desplazamiento del jugador en el aire
    [SerializeField] private float velocidadEnAire = 10f;

    // Velocidad de desaceleración
    [SerializeField] private float velocidadDesaceleracion = 2f;

    // Tiempo máximo de salto
    [SerializeField] private float tiempoSaltoMaximo = 0.5f;

    // Tiempo actual de salto
    [SerializeField] private float tiempoSaltoActual = 0f;

    // Fuerza de salto
    [SerializeField] private float fuerzaSalto = 15f;

    // Variable booleana que indica si el jugador está colisionando con el terreno
    private bool isColisionTerreno;

    // Variable que indica si el jugador ha pulsado la tecla espacio
    private bool isPulsadoSalto;

    // Estado actual del salto
    private ESTADO_SALTO estadoSalto = ESTADO_SALTO.EN_TIERRA;

    private Rigidbody2D rigidbody2d;

    [SerializeField] private Vector2 posicionAnterior;
    [SerializeField] private float distanciaRecorrida = 0;
    [SerializeField] private float distanciaLimite = 20f;

    [SerializeField]  private bool puedeMoverse=false;


    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        posicionAnterior = this.transform.position;
    }

    void FixedUpdate()
    {
        bool isPulsadoSalto = Input.GetKey(KeyCode.Space) == true;
           
        float horizontal = Input.GetAxis("Horizontal");


        if (puedeMoverse)
        {

            if (isColisionTerreno)
            {
                estadoSalto = ESTADO_SALTO.EN_TIERRA;
            }

            else if (estadoSalto != ESTADO_SALTO.SALTANDO)
            {
                estadoSalto = ESTADO_SALTO.CAYENDO;
            }



            if (isColisionTerreno)
            {
                rigidbody2d.AddForce(new Vector2(horizontal * velocidadMovimiento, 0));
            }
            else
            {
                rigidbody2d.AddForce(new Vector2(horizontal * velocidadEnAire, 0));
            }


            // Control de la desaceleración del jugador.
            // Comprobamos que esté colisionando con el terreno antes de desacelerar
            if (isColisionTerreno && Mathf.Approximately(horizontal, 0))
            {
                rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x * -velocidadDesaceleracion, 0));
            }


            if (isPulsadoSalto)
            {
                if ((estadoSalto == ESTADO_SALTO.EN_TIERRA || estadoSalto == ESTADO_SALTO.SALTANDO) && tiempoSaltoActual < tiempoSaltoMaximo)
                {
                    estadoSalto = ESTADO_SALTO.SALTANDO;
                    rigidbody2d.AddForce(new Vector2(0, fuerzaSalto));
                    tiempoSaltoActual += Time.deltaTime;
                }
            }
            else if (estadoSalto == ESTADO_SALTO.EN_TIERRA)
            {
                tiempoSaltoActual = 0;
            }


            if (estadoSalto == ESTADO_SALTO.SALTANDO && (tiempoSaltoActual >= tiempoSaltoMaximo || !isPulsadoSalto))
            {
                estadoSalto = ESTADO_SALTO.CAYENDO;
            }

        }
        distanciaRecorrida += Vector2.Distance(transform.position, posicionAnterior);
            posicionAnterior = transform.position;
        

    }

    
    protected void OnCollisionEnter2D(Collision2D other)
    {
        isColisionTerreno = true;
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        isColisionTerreno = false;
    }
    public bool isLimitePasos()
    {
        return distanciaRecorrida > distanciaLimite;
    }

    public void saltar(float cantidad)
    {
        rigidbody2d.AddForce(new Vector2(0, cantidad));

    }
    public void efectoRayo(float numCantidadPasosAdd, float velocidadAumentar)
    {
        this.distanciaLimite += numCantidadPasosAdd;
        this.velocidadMovimiento += velocidadAumentar;
    }
    public void endMovement(){
		this.distanciaRecorrida = distanciaLimite + 1;
	}

    public bool PuedeMoverse
    {
        get
        {
            return puedeMoverse;
        }

        set
        {
            this.distanciaRecorrida = 0;
            puedeMoverse = value;
        }
    }

    public float VelocidadMovimiento
    {
        get
        {
            return velocidadMovimiento;
        }

        set
        {
            velocidadMovimiento = value;
        }
    }

    public float VelocidadEnAire
    {
        get
        {
            return velocidadEnAire;
        }

        set
        {
            velocidadEnAire = value;
        }
    }

    public float TiempoSaltoMaximo
    {
        get
        {
            return tiempoSaltoMaximo;
        }

        set
        {
            tiempoSaltoMaximo = value;
        }
    }

    public float TiempoSaltoActual
    {
        get
        {
            return tiempoSaltoActual;
        }

        set
        {
            tiempoSaltoActual = value;
        }
    }

    public float FuerzaSalto
    {
        get
        {
            return fuerzaSalto;
        }

        set
        {
            fuerzaSalto = value;
        }
    }

    public float VelocidadMovimiento1
    {
        get
        {
            return velocidadMovimiento;
        }

        set
        {
            velocidadMovimiento = value;
        }
    }

    public bool ColisionaConTerreno()
    {
        return isColisionTerreno;
    }
}