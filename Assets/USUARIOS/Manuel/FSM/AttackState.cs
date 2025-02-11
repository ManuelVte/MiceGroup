using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [Header("Estado de Ataque")]
    [Tooltip("Establece el daño base que realiza el enemigo.")]
    [SerializeField] private float baseAttackDamage;
    private AN_HeroController anHeroController;

    private Rigidbody jugador;

    public override void OnEnterState(EnemyController controller)
    {
        //Debug.Log("AttackState.cs - OnEnterState() - INICIO.");

        base.OnEnterState(controller);

        controller.Agent.stoppingDistance = controller.DistanciaAtaque;
        controller.Anim.SetBool("attacking", true);
        // FUNCIÓN PEDRO:
        anHeroController = FindAnyObjectByType<AN_HeroController>();
        if (anHeroController != null )
        {
            anHeroController.LifeLoss(15);
        }
        else
        {
            Debug.LogError("No encontrado el hero controller");
        }
    }

    public override void OnUpdateState()
    {
        // Hacer daño y Encarar al objetivo nuevamente.
        Encarar();
    }

    private void Encarar()
    {
        Vector3 directionToTarget = (controller.Target.transform.position - transform.position).normalized;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    public override void OnExitState()
    {
        
    }

    public void OnFinishAttackAnimation()
    {
        //Debug.Log("AttackState.cs - OnEnterState() - INICIO.");

        // Si el jugador está fuera de rango, cambiar estado a perseguir. (ChaseState)
        if (Vector3.Distance(transform.position, controller.Target.transform.position) > controller.DistanciaAtaque)
        {
            controller.Anim.SetBool("attacking", false);
            controller.ChangeState(controller.ChaseState);
        }
    }
}
