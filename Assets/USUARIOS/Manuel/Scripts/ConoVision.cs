using Unity.VisualScripting;
using UnityEngine;

public class ConoVision : MonoBehaviour
{
    [Header("Parámetros para Ajustar el Cono de Visión")]
    [Tooltip("Aplica el Material deseado al Cono de Visión.")]
    [SerializeField] public Material visionConeMaterial;
    [Tooltip("Aumenta o Disminuye el alcance del Cono de Visión.")]
    [SerializeField] public float visionRange = 15;
    [Tooltip("Amplia o Reduce el Campo de Visión.")]
    [SerializeField] public float visionAngle = 125;
    [Tooltip("Selecciona la Capa por la cual será Obstruida el Cono de Visión.")]
    [SerializeField] public LayerMask visionObstructingLayer;    // Capa que impedirá que se vea a través de las paredes, por si hay que esconderse.
    [Tooltip("Añade triángulos para mejorar la circunferencia del cono de visión, a más alto más circular sin pasar de 360.")]
    [SerializeField] public int visionConeResolution = 120;      // Triángulos que se le añade al cono para perfeccionar la curvatura de la circunferencia. (Más alto, más redondo).
    Mesh visionConeMesh;
    MeshFilter filtrerMesh;

    private EnemyController controller; // Referencia al EnemyController

    void Start()
    {

        controller = GetComponentInParent<EnemyController>(); // Obtener referencia
        //controller = GetComponent<EnemyController>();

        if (controller == null)
        {
            Debug.LogError("ConoVision: No se encontró EnemyController en el objeto padre.");
            return;
        }

        gameObject.AddComponent<MeshRenderer>().material = visionConeMaterial;
        filtrerMesh = gameObject.AddComponent<MeshFilter>();
        visionConeMesh = new Mesh();

        //transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        //MeshFilter_ = transform.AddComponent<MeshFilter>();
        //VisionConeMesh = new Mesh();
        //VisionAngle *= Mathf.Deg2Rad;
    }

    void Update()
    {
        controller = GetComponentInParent<EnemyController>();

        if (controller != null)
        {
            DrawVisionCone();
        }
    }

    void DrawVisionCone()
    {

        visionRange = controller.RangoVision;
        visionAngle = controller.AnguloVision * Mathf.Deg2Rad;

        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[visionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float currentAngle = -visionAngle / 2;
        float angleIncrement = visionAngle / (visionConeResolution - 1);
        float seno, coseno;

        for (int i = 0; i < visionConeResolution; i++)
        {
            seno = Mathf.Sin(currentAngle);
            coseno = Mathf.Cos(currentAngle);
            Vector3 RaycastDirection = (transform.forward * coseno) + (transform.right * seno);
            Vector3 VertForward = (Vector3.forward * coseno) + (Vector3.right * seno);

            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, visionRange, visionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * visionRange;
            }

            currentAngle += angleIncrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        visionConeMesh.Clear();
        visionConeMesh.vertices = Vertices;
        visionConeMesh.triangles = triangles;
        filtrerMesh.mesh = visionConeMesh;
    }
}