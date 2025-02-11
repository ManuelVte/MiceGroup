using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    [Header("Parámetros de los Enemigos")]
    [Tooltip("Ajusta el Rango de Visión de los Enemigos.")]
    [SerializeField] private float rangoVision;
    [Tooltip("Ajusta el Ángulo de Visión de los Enemigos.")]
    [SerializeField] private float anguloVision;
    [Tooltip("Ajusta la Distancia de Ataque de los Enemigos.")]
    [SerializeField] private float distanciaAtaque;
    [Tooltip("Ajusta Velocidad Máxima a la que van los Enemigos.")]
    [SerializeField] private float velocidadMaxima;
    [Tooltip("Asigna quién es el Objetivo de los Enemigos.")]
    [SerializeField] private LayerMask queEsTarget;
    [Tooltip("Asigna qué es un Obstáculo para los Enemigos.")]
    [SerializeField] private LayerMask queEsObstaculo;

    private State<EnemyController> currentState;
    private NavMeshAgent agent;

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;
    private Transform target;
    private Animator anim;

    #region getters & setters
    public NavMeshAgent Agent { get => agent; }
    public float RangoVision { get => rangoVision; }
    public LayerMask QueEsTarget { get => queEsTarget; }
    public LayerMask QueEsObstaculo { get => queEsObstaculo;}
    public float AnguloVision { get => anguloVision; }
    public PatrolState PatrolState { get => patrolState; }
    public ChaseState ChaseState { get => chaseState;  }
    public AttackState AttackState { get => attackState;}
    public Transform Target { get => target; set => target = value; }
    public float DistanciaAtaque { get => distanciaAtaque; }// set => distanciaAtaque = value; }
    public Animator Anim { get => anim; }
    public float VelocidadMaxima { get => velocidadMaxima; }// set => velocidadMaxima = value; }
    #endregion

    private void Awake()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();

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

        //Debug.Log($"El nuevo estado es: {newState}.");

        if(currentState != null && currentState != newState)
        {
            currentState.OnExitState();
        }
        currentState = newState; //Mi estado actual pasa a ser el nuevo.
        currentState.OnEnterState(this);
    }
}
