using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    private AN_HeroController heroController;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Buscar el jugador
        if (player != null)
        {
            heroController = player.GetComponent<AN_HeroController>(); // Obtener el HeroController
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (heroController != null)
            {
                heroController.LifeLoss(25); // Llamar LifeLoss() solo si heroController no es null
            }
            else
            {
                Debug.LogError("HeroController no encontrado en el Player.");
            }
        }
    }
}
