
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody; // Referencia al cuerpo del jugador

    private float rotationSpeed = 4f;
    private float xAxis = 0f, yAxis = 0f;

    // Límites de rotación en el eje Y (vertical)
    private float minY = -80f, maxY = 80f;

    void Start()
    {
        // Asegurar que la cámara comience en la rotación correcta
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; // Oculta el cursor

        // Iniciar con la rotación actual del jugador
        xAxis = playerBody.eulerAngles.y;
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotación horizontal (movemos el cuerpo del jugador)
        xAxis += mouseX;

        // Rotación vertical (movemos solo la cámara)
        yAxis -= mouseY;
        yAxis = Mathf.Clamp(yAxis, minY, maxY); // Limitamos la cámara para que no se voltee

        // Aplicamos la rotación
        playerBody.rotation = Quaternion.Euler(0f, xAxis, 0f); // Rotación horizontal del jugador
        transform.localRotation = Quaternion.Euler(yAxis, 0f, 0f); // Rotación vertical de la cámara
    }
}