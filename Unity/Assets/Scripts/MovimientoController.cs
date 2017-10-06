
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
    public float velocidadMovimiento = 10f;

    // Velocidad de desplazamiento del jugador en el aire
    public float velocidadEnAire = 10f;

    // Velocidad de desaceleración
    public float velocidadDesaceleracion = 2f;

    // Tiempo máximo de salto
    public float tiempoSaltoMaximo = 0.5f;

    // Tiempo actual de salto
    private float tiempoSaltoActual = 0f;

    // Fuerza de salto
    public float fuerzaSalto = 15f;


    // Variable booleana que indica si el jugador está colisionando con el terreno
    private bool isColisionTerreno;

    // Variable que indica si el jugador ha pulsado la tecla espacio
    private bool isPulsadoSalto;

    // Estado actual del salto
    private ESTADO_SALTO estadoSalto = ESTADO_SALTO.EN_TIERRA;

    private Rigidbody2D rigidbody2d;


    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool isPulsadoSalto = Input.GetKey(KeyCode.Space) == true;
        float horizontal = Input.GetAxis("Horizontal");

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


    protected void OnCollisionEnter2D(Collision2D other)
    {
        isColisionTerreno = true;
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        isColisionTerreno = false;
    }

}