using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        Debug.Log("Entro en el estado de perseguir!");
    }

    public override void OnUpdateState()
    {
        
    }

    public override void OnExitState()
    {
       
    }


}
