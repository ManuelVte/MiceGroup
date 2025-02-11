using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    [Header("Estado de Patrulla")]
    //[Tooltip("Configura la velocidad de persecución.")]
    //[SerializeField] private float velocidadChase;
    [Tooltip("Configura el tiempo que transcurre antes de volver a patrullar tras la persecución si pierde al objetivo.")]
    [SerializeField] private float tiempoAntesDePatrullar;

    private Coroutine coroutine;

    public override void OnEnterState(EnemyController controller)
    {
        //Debug.Log("ChaseState.cs - OnEnterState() - INICIO.");

        base.OnEnterState(controller);

        controller.Agent.stoppingDistance = controller.DistanciaAtaque;
        controller.Agent.speed = controller.VelocidadMaxima;
    }

    public override void OnUpdateState()
    {
        controller.Anim.SetFloat("velocity", controller.Agent.velocity.magnitude / controller.VelocidadMaxima);
        // Sólo si el objetivo es alcanzable...
        if (!controller.Agent.pathPending && controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath()))
        {
            StopMyCoroutine();

            controller.Agent.SetDestination(controller.Target.position);

            // No tengo cálculos pendientes y mi distancia hacia mi objetivo está por debajo de mi distancia de parada.
            if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
            {
                controller.ChangeState(controller.AttackState);
            }
        }
        else
        {
            coroutine ??= StartCoroutine(StopAndReturn());
        }
    }

    private void StopMyCoroutine()
    {
        StopAllCoroutines();
        coroutine = null;
    }

    // Se para y si no vuelves al área, se volverá a patrullar.
    private IEnumerator StopAndReturn()
    {
        yield return new WaitForSeconds(tiempoAntesDePatrullar);
        controller.ChangeState(controller.PatrolState);
    }

    public override void OnExitState()
    {
        StopMyCoroutine();
    }
}
