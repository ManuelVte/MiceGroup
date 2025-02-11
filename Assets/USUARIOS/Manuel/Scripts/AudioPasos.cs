using UnityEngine;

public class AudioPasos : MonoBehaviour
{
    [Header("Sonidos de Estado")]
    [Tooltip("Añade los audios del sonido Reposo para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] sonidosReposo;
    [Tooltip("Añade los audios del sonido Ataque para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] sonidosAtaque;

    [Header("Sonidos de Movimiento")]
    [Tooltip("Añade los audios del sonido Caminando para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] pasosCaminando;
    [Tooltip("Añade los audios del sonido Corriendo para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] pasosCorriendo;
    [Tooltip("Añade los audios del sonido Saltando para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] sonidosSaltando;
    [Tooltip("Añade los audios del sonido Aterrizando para su ejecución aleatoria.")]
    [SerializeField] private AudioClip[] sonidosAterrizando;

    [Header("Configuración")]
    [Tooltip("")]
    [SerializeField] private float intervaloCaminar = 0.5f;
    [Tooltip("")]
    [SerializeField] private float intervaloCorrer = 0.3f;
    [Tooltip("Ajustar el parámetro para detectar si el jugador se está moviendo. (En el CC suele ser 0.1f, pero el RB ni idea.")]
    [SerializeField] private float sensivilidadMoviendose = 0.1f;
    [Tooltip("Activar esta opción si el Script lo tiene vinculado el jugador.")]
    [SerializeField] private bool esJugador = false;

    private CharacterController cc;
    private Rigidbody rb;
    private AudioSource audioSource;

    // Para detectar si aterriza.
    private bool estabaEnElSuelo = true;

    private float temporizadorPasos = 0f;


    void Start()
    {
        //Debug.Log("AudioPasos.cs - Start() - INICIO.");

        // Obtiene información sobre si existe alguno de los componentes y te informa sobre ello.
        cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            Debug.Log("Se ha encontrado un Character Controller.");
        }
        else
        {
            //Debug.LogError("No se ha encontrado ningún Character Controller.");
        }

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.LogError("Se ha encontrado un RigiBody.");
        }
        else
        {
            //Debug.LogError("No se ha encontrado ningún RigiBody.");
        }

        // Si no encuentra el componente, lo agregará para generar las pisadas.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (esJugador) ProcesoMovimientoJugador();
    }

    private void ProcesoMovimientoJugador()
    {
        // Aquí se realizará todo el proceso necesario para la ejecución del código del jugador.

        bool estaEnElSuelo = DetectarSuelo();
        bool estaMoviendose = ObtenerVelocidad() > sensivilidadMoviendose;

        bool estaCorriendo = Input.GetKey(KeyCode.LeftShift);
        bool estaSaltando = Input.GetButtonDown("Jump");

        // Reproducir sonido de pasos si el jugador está en movimiento.
        if (estaEnElSuelo && estaMoviendose)
        {
            temporizadorPasos += Time.deltaTime;

            float intervaloActual = estaCorriendo ? intervaloCorrer : intervaloCaminar;
            AudioClip[] pasosActuales = estaCorriendo ? pasosCorriendo : pasosCaminando;

            if (temporizadorPasos >= intervaloActual)
            {
                ReproducirSonidoAleatorio(pasosActuales);
                temporizadorPasos = 0f;
            }
        }

        // Detectar inicio de salto
        if (estaSaltando && estaEnElSuelo)
        {
            ReproducirSonidoAleatorio(sonidosSaltando);
        }

        // Detectar aterrizaje
        if (!estabaEnElSuelo && estaEnElSuelo)
        {
            ReproducirSonidoAleatorio(sonidosAterrizando);
        }

        // Actualizar estado del suelo
        estabaEnElSuelo = estaEnElSuelo;
    }

    private bool DetectarSuelo()
    {
        if (cc != null)
        {
            return cc.isGrounded;
        }

        if (rb != null)
        {
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
        }

        return false;
    }

    [System.Obsolete]     //Solo es necesario para el RigiBody, el Velocity del CC no está obsoleto. Quizás se pueda usar LinearVelocity.
    private float ObtenerVelocidad()
    {
        if (cc != null)
        {
            return cc.velocity.magnitude;
        }

        if (rb != null)
        {
            return rb.velocity.magnitude;
        }

        return 0f;
    }

    private void ReproducirSonidoAleatorio(AudioClip[] clips)
    {
        if (clips.Length > 0)
        {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    // SONIDOS DE MOVIMIENTO
    public void AudioWalkingAnimation()
    {
        ReproducirSonidoAleatorio(pasosCaminando);
    }

    public void AudioRunningAnimation()
    {
        ReproducirSonidoAleatorio(pasosCorriendo);
    }

    public void AudioJumpingAnimation()
    {
        ReproducirSonidoAleatorio(sonidosSaltando);
    }

    public void AudioLandingAnimation()
    {
        ReproducirSonidoAleatorio(sonidosAterrizando);
    }

    // SONIDOS DE ESTADO
    public void AudioIdleAnimation()
    {
        ReproducirSonidoAleatorio(sonidosReposo);
    }
    public void AudioAttackAnimation()
    {
        ReproducirSonidoAleatorio(sonidosAtaque);
    }
}