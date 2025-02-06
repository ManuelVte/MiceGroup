using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log("Entramos en ataque! >.<");

    }
    public override void OnUpdateState()
    {
        
    }
    public override void OnExitState()
    {
        
    }

}
