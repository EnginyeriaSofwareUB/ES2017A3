
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
    public float velocidadMovimiento = 5f;

    // Velocidad de desplazamiento del jugador en el aire
    public float velocidadEnAire = 8f;

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

    void Move()
    {
        PlayerState move = PlayerController.currentPlayerState;
        Vector3 direction;
        switch (move)
        {
            case PlayerState.RIGHT:
                direction = Vector3.right;
                break;
            case PlayerState.LEFT:
                direction = Vector3.left;
                break;
            case PlayerState.JUMP:
                direction = Vector3.up;
                break;
            default:
                direction = new Vector3(1, 1, 1);
                break;
        }

        transform.Translate(direction * velocidadMovimiento * Time.deltaTime);
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