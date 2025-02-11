using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State<EnemyController>
{
    [Header("Estado de Patrulla")]
    [Tooltip("Toma el transform que asignará como ruta.")]
    [SerializeField] private Transform ruta;
    [Tooltip("Configura la velocidad de patrulla.")]
    [SerializeField] private float velocidadPatrol;
    [Tooltip("Configura el tiempo que esperará entre un punto y otro de la patrullará.")]
    [SerializeField] private float tiempoDeEspera;


    private List<Vector3> puntosDeRuta = new List<Vector3>();
    private int indicePuntoActual = 0; //Marca el �ndice de la lista

    private Vector3 destinoActual; //Marca mi destino actual.

    //private const int ID_WALINKG = 0;



    /*public Material VisionConeMaterial;
    public LayerMask VisionObstructingLayer;
    public int VisionConeResolution = 120;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;

    private void Start()
    {
        // Verifica si el objeto ya tiene un MeshRenderer y úsalo en lugar de agregar uno nuevo.
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }
        renderer.material = VisionConeMaterial;

        // Verifica si ya tiene un MeshFilter, si no, lo agrega.
        MeshFilter_ = gameObject.GetComponent<MeshFilter>();
        if (MeshFilter_ == null)
        {
            MeshFilter_ = gameObject.AddComponent<MeshFilter>();
        }

        VisionConeMesh = new Mesh();

        //gameObject.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        //MeshFilter_ = gameObject.AddComponent<MeshFilter>();
        //VisionConeMesh = new Mesh();
    }*/

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        foreach (Transform punto in ruta)
        {
            puntosDeRuta.Add(punto.position);
        }

        destinoActual = puntosDeRuta[indicePuntoActual];

        controller.Agent.stoppingDistance = 0f;
        controller.Agent.speed = velocidadPatrol;

        //ID_WALINKG = Animator.StringToHash("walking");

        StartCoroutine(PatrullarYEsperar());
    }

    // Cono de visión sin dibujar.
    public override void OnUpdateState()
    {
        controller.Anim.SetFloat("velocity", controller.Agent.velocity.magnitude / controller.VelocidadMaxima);
        //DrawVisionCone();
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, controller.RangoVision, controller.QueEsTarget);
        if (collsDetectados.Length > 0) //Hay al menos un target dentro del rango. //1
        {
            Vector3 direccionATarget = (collsDetectados[0].transform.position - transform.position).normalized;

            if (!Physics.Raycast(transform.position, direccionATarget, controller.RangoVision, controller.QueEsObstaculo)) //2
            {
                if (Vector3.Angle(transform.forward, direccionATarget) <= controller.AnguloVision / 2)
                {
                    controller.Target = collsDetectados[0].transform;
                    controller.ChangeState(controller.ChaseState);
                }
            }

        }
    }

    public override void OnExitState()
    {
        StopAllCoroutines();
    }

    private IEnumerator PatrullarYEsperar()
    {
        while (true)
        {
            controller.Agent.SetDestination(destinoActual); //Voy yendo al destino
            //controller.Anim.SetBool(ID_WALINKG, true);
            yield return new WaitUntil(() => !controller.Agent.pathPending && controller.Agent.remainingDistance <= 0.2f); // ESPERO en este punto hasta que llegue.
            //controller.Anim.SetBool(ID_WALINKG, false);
            yield return new WaitForSeconds(tiempoDeEspera); //Me espero en dicho punto.
            CalcularNuevoDestino();

        }
    }

    private void CalcularNuevoDestino()
    {
        indicePuntoActual++; //Avanzamos uno.
        indicePuntoActual = indicePuntoActual % (puntosDeRuta.Count); //Me aseguro de no salirme de los puntos m�ximos.
        destinoActual = puntosDeRuta[indicePuntoActual]; //Actualizo mi destino actual.

    }
}



/*void DrawVisionCone()
{
    float VisionRange = controller.RangoVision;
    float VisionAngle = controller.AnguloVision * Mathf.Deg2Rad;

    int[] triangles = new int[(VisionConeResolution - 1) * 3];
    Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
    Vertices[0] = Vector3.zero;
    float Currentangle = -VisionAngle / 2;
    float angleIncrement = VisionAngle / (VisionConeResolution - 1);
    float Sine, Cosine;

    for (int i = 0; i < VisionConeResolution; i++)
    {
        Sine = Mathf.Sin(Currentangle);
        Cosine = Mathf.Cos(Currentangle);
        Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
        Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);

        if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
        {
            Vertices[i + 1] = VertForward * hit.distance;
        }
        else
        {
            Vertices[i + 1] = VertForward * VisionRange;
        }

        Currentangle += angleIncrement;
    }

    for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
    {
        triangles[i] = 0;
        triangles[i + 1] = j + 1;
        triangles[i + 2] = j + 2;
    }

    VisionConeMesh.Clear();
    VisionConeMesh.vertices = Vertices;
    VisionConeMesh.triangles = triangles;
    MeshFilter_.mesh = VisionConeMesh;
}*/