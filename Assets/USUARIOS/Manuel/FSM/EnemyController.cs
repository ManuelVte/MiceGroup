using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    [SerializeField] private float rangoVision;
    [SerializeField] private float anguloVision;
    [SerializeField] private LayerMask queEsTarget;
    [SerializeField] private LayerMask queEsObstaculo;

    private State<EnemyController> currentState;
    private NavMeshAgent agent;

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    #region getters & setters
    public NavMeshAgent Agent { get => agent; }
    public float RangoVision { get => rangoVision; }
    public LayerMask QueEsTarget { get => queEsTarget; }
    public LayerMask QueEsObstaculo { get => queEsObstaculo;}
    public float AnguloVision { get => anguloVision; }
    public PatrolState PatrolState { get => patrolState; }
    public ChaseState ChaseState { get => chaseState;  }
    public AttackState AttackState { get => attackState;}
    #endregion

    private void Awake()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
        agent = GetComponent<NavMeshAgent>();

        ChangeState(patrolState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.OnUpdateState();
        }
    }
    public void ChangeState(State<EnemyController> newState)
    {
        if(currentState != null && currentState != newState)
        {
            currentState.OnExitState();
        }
        currentState = newState; //Mi estado actual pasa a ser el nuevo.
        currentState.OnEnterState(this);
    }
}
