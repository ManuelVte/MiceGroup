using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverM : MonoBehaviour
{
    //FUNCIONES

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //BOT�N REINICIAR
    public void RestardGame()
    {
        //Carga escena del MainMenu tras al pulsar el bot�n de "REINICIAR"
        SceneManager.LoadScene(0);
    }
    //BOT�N SALIR
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit();
    }
}
