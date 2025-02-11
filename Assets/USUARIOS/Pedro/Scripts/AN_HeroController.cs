using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AN_HeroController : MonoBehaviour
{
    [Tooltip("Character settings (rigid body)")]
    public float MoveSpeed = 30f, JumpForce = 200f, Sensitivity = 70f;
    bool jumpFlag = true; // to jump from surface only

    CharacterController character;
    Rigidbody rb;
    Vector3 moveVector;

    Transform Cam;
    float yRotation;

    public float groundCheckDistance;
    public LayerMask groundLayer;
    public float life = 100f;
    public HealthBar healthBar;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();
        healthBar = FindAnyObjectByType<HealthBar>();
        Cursor.lockState = CursorLockMode.Locked; // freeze cursor on screen centre
        Cursor.visible = false; // invisible cursor
    }

    void Update()
    {
        // camera rotation
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        transform.Rotate(Vector3.up * xmouse);
        yRotation -= ymouse;
        yRotation = Mathf.Clamp(yRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(yRotation, 0, 0);

        jumpFlag = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && jumpFlag)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            jumpFlag = false; // Evita que vuelva a saltar hasta que toque el suelo
        }
    }

    void FixedUpdate()
    {
        // body moving
        moveVector = transform.forward * MoveSpeed * Input.GetAxis("Vertical") +
            transform.right * MoveSpeed * Input.GetAxis("Horizontal") +
            transform.up * rb.linearVelocity.y;
        rb.linearVelocity = moveVector;

        
    }
    
    private void OnTriggerStay(Collider other)
    {
        jumpFlag = true; // hero can jump
    }

    private void OnTriggerExit(Collider other)
    {
        jumpFlag = false;
    }
    public void LifeLoss(int damage)
    {
        life -= damage;
        healthBar.HealthDrain();
        Debug.Log(life);
        if (life <= 0)
        {
            life = 0;
            SceneManager.LoadScene(3);
            Debug.Log("Muerto");
        }
    }
}
