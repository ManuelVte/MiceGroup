using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIandPM : MonoBehaviour
{
    //VARIABLES
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject pauseMenu;
    private bool gameStop = false;

    //FUNCIONES

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){

            if (gameStop)
            {
                Continuar();
            }
            else
            {
                Pausa();
            }
        }
    }
    //BOT�N PAUSAR
    public void Pausa()
    {
        //Para el juego. El bot�n de PAUSA de desactiva y aparece el men� de PAUSA
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameStop = true;
        Time.timeScale = 0f;
        buttonPause.SetActive(false);
        pauseMenu.SetActive(true);

    }

    //BOT�N CONTINUAR
    public void Continuar()
    {
        //Continua el juego. El bot�n de PAUSA se activa y desaparece el men� de PAUSA
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameStop = false;
        Time.timeScale = 1f;
        buttonPause.SetActive(true);
        pauseMenu.SetActive(false);

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
