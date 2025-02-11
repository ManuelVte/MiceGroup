using UnityEngine;

public class Spawner_Enemys : MonoBehaviour
{
    [Header("Configuración del Spawner de los Enemigos")]
    [Tooltip("Prefab de la zona donde los enemigos spawnearán.")]
    [SerializeField] public GameObject zonaSpawn;
    [Tooltip("Prefab de los enemigos que se van a crear.")]
    [SerializeField] public GameObject[] enemigoSpawn;
    [Tooltip("Asigna el número de enemigos que se crearán en escena.")]
    [SerializeField] public int numEnemigos = 5;
    [Tooltip("Altura extra para que el enemigo no se cree dentro del suelo (No pongas un número negativo, que te veo. ¬¬)")]
    [SerializeField] public float alturaExtra = 2.0f;
    [Tooltip("Radio para evitar colisiones al spawnear. (Tener en cuenta la altura del enemigo y la altura asignada como extra desde el suelo)")]
    [SerializeField] public float radioColision = 1.0f;

    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        // Sé lo poco que te gusta el Find("String"), a mi también me gusta poco o nada, pero para añadir un object sin script y no crear uno en blanco, lo hago en sucio.
        // Bien podría ser un arrastrable con public gameObject floor; pero estaba probando varias cosas
        // y necesitaba cambiar rápidamente sin estar cambiando constantemente el objeto en concreto, puesto que estaba probando distintos suelos.
        // Además, como soy el encargado de crear los enemigos y hacer que se muevan por el jardín, si alguien me mueve la escena, no me tengo que preocupar,
        // porque estoy creándolos localizando el sitio donde debo depositarlos y dándoles rutas aleatorias, también evitando que no haya ningún otro objeto a la hora de crearlo.
        //GameObject suelo = GameObject.Find("Floor");

        if (zonaSpawn == null)
        {
            Debug.LogError("No se encontró el objeto 'Floor' en la escena.");
            return;
        }

        Collider sueloCollider = zonaSpawn.GetComponent<Collider>();

        if (sueloCollider == null)
        {
            Debug.LogError("El objeto 'Floor' no tiene un Collider.");
            return;
        }

        // Obtener los límites del suelo
        Bounds limites = sueloCollider.bounds;

        for (int i = 0; i < numEnemigos; i++)
        {
            Vector3 posicionSpawn;
            int intentos = 100; // Evita que se generen bucles infinitos por algún error de cálculos, que se haya creado un objeto en toda la zona o lo que sea...

            do
            {
                float randomX = Random.Range(limites.min.x, limites.max.x);
                float randomZ = Random.Range(limites.min.z, limites.max.z);
                float altura = limites.max.y + alturaExtra;

                posicionSpawn = new Vector3(randomX, altura, randomZ);
                intentos--;

                // Crear MIENTRAS no colisione con un objeto al intentar crear un enemigo en la posición hasta crear todos los enemigos y siempre que el número de intentos sea superior a 0.
            } while (Physics.OverlapSphere(posicionSpawn, radioColision).Length > 0 && intentos > 0);

            // Seleccionar un enemigo, de todos los agregados, aleatorio del array.
            if (enemigoSpawn.Length > 0)
            {
                GameObject enemigo = enemigoSpawn[Random.Range(0, enemigoSpawn.Length)];
                Instantiate(enemigo, posicionSpawn, Quaternion.identity);
            }
        }
    }
}
