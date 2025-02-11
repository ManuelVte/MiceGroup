//using UnityEngine;

//public class PlayerDynamics : MonoBehaviour
//{
//    private CharacterController characterController;
//    private float hInput;
//    private float vInput;
//    private Vector3 velocity;

//    [SerializeField] private float movementSpeed = 5f;
//    [SerializeField] private Transform cameraTransform;
//    [SerializeField] private float jumpForce = 5f;
//    [SerializeField] private float gravity = 9.81f;
//    [SerializeField] private float jumpForwardForce = 3f; // Impulso extra en Z al saltar

//    PARA AÑADIR PEDRO
//    #region Puertas y trampas
//   [Tooltip("Tienes alguna llave?")]
//    public bool RedKey = false, BlueKey = false;
//    [Tooltip("Objeto hijo vacío para el seguimiento del plug")]
//    public Transform GoalPosition;
//    #endregion



//    void Start()
//    {
//        characterController = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        // Movimiento lateral basado en la cámara
//        hInput = Input.GetAxis("Horizontal");
//        vInput = Input.GetAxis("Vertical");

//        Vector3 forward = cameraTransform.forward;
//        Vector3 right = cameraTransform.right;

//        forward.y = 0; // Evitar inclinaciones
//        right.y = 0;
//        forward.Normalize();
//        right.Normalize();

//        Vector3 moveDirection = (forward * vInput + right * hInput).normalized * movementSpeed;

//        // Verificar si está en el suelo
//        bool isGrounded = characterController.isGrounded;

//        if (isGrounded)
//        {
//            if (velocity.y < 0)
//            {
//                velocity.y = -2f; // Resetear la velocidad vertical para evitar acumulación
//            }

//            // ⬇️ **Resetear velocidad horizontal al aterrizar para eliminar inercia** ⬇️
//            velocity.x = 0;
//            velocity.z = 0;

//            // Salto con impulso en la dirección en la que mira el jugador
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity); // Salto vertical
//                velocity += forward * jumpForwardForce; // Impulso hacia adelante solo al saltar
//            }
//        }

//        // Aplicar gravedad
//        velocity.y -= gravity * Time.deltaTime;

//        // Mover al jugador
//        Vector3 finalMove = moveDirection + velocity;
//        characterController.Move(finalMove * Time.deltaTime);
//    }
//}
