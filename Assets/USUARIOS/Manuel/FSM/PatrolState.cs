using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State<EnemyController>
{
    [SerializeField]
    private Transform ruta;

    [SerializeField]
    private float tiempoDeEspera;


    private List<Vector3> puntosDeRuta = new List<Vector3>();
    private int indicePuntoActual = 0; //Marca el índice de la lista

    private Vector3 destinoActual; //Marca mi destino actual.

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        foreach (Transform punto in ruta)
        {
            puntosDeRuta.Add(punto.position);
        }

        destinoActual = puntosDeRuta[indicePuntoActual];
        StartCoroutine(PatrullarYEsperar());
    }
    public override void OnUpdateState()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, controller.RangoVision, controller.QueEsTarget);
        if (collsDetectados.Length > 0) //Hay al menos un target dentro del rango. //1
        {
            Vector3 direccionATarget = (collsDetectados[0].transform.position - transform.position).normalized;

            if (!Physics.Raycast(transform.position, direccionATarget, controller.RangoVision, controller.QueEsObstaculo)) //2
            {
                if (Vector3.Angle(transform.forward, direccionATarget) <= controller.AnguloVision / 2)
                {
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
            yield return new WaitUntil(() => !controller.Agent.pathPending && controller.Agent.remainingDistance <= 0.2f); //mE ESPERO en este punto hasta que llegue.
            yield return new WaitForSeconds(tiempoDeEspera); //Me espero en dicho punto.
            CalcularNuevoDestino();

        }
    }

    private void CalcularNuevoDestino()
    {
        indicePuntoActual++; //Avanzamos uno.
        indicePuntoActual = indicePuntoActual % (puntosDeRuta.Count); //Me aseguro de no salirme de los puntos máximos.
        destinoActual = puntosDeRuta[indicePuntoActual]; //Actualizo mi destino actual.

    }



}
