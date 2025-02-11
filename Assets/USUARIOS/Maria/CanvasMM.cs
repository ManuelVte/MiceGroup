using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{ 

//VARIABLES
[SerializeField] private GameObject mainMenuPanel;
[SerializeField] private GameObject optionsMenuPanel;
[SerializeField] private AudioMixer masterMixer;

//FUNCIONES

    //BOT�N JUGAR
    public void PlayGame()
    {

        //Carga escena del juego tras pulsar bot�n de "JUGAR"
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //BOT�N SALIR
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit();
    }

    //BOT�N OPCIONES
    //Se ejecuta al pulsar sobre el bot�n de "OPCIONES".
   public void OnOptionsButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    //MEN� OPCIONES - BOT�N VOLVER
    public void OnReturnOptionsButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            mainMenuPanel.SetActive(true);
            optionsMenuPanel.SetActive(false);
        }
    }

    public void SetVolumeMusic (float volume)
    {
        masterMixer.SetFloat("musicVolume", volume);
    }

    public void SetVolumeSounds(float volume)
    {
        masterMixer.SetFloat("soundsVolume", volume);
    }
}
