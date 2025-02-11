using UnityEngine;

public class AnimEventHandler : MonoBehaviour
{
    // Se crea un script exclusivo para que tras el ataque se pueda llamar a la función, ya que el Script del AttackState y el Animator no están en el mismo GameObject,
    // por lo tanto tiene que tener un enlace para poder realizar la acción requerida.

    // Busca el script en el padre para tener acceso y ejecutarlo
    void AlFinalizarAnimacionAtaque()
    {
        // Debug.Log("AnimEventHandler.cs - AlFinalizarAnimacionAtaque() - INICIO.");
        transform.parent.GetComponent<AttackState>().OnFinishAttackAnimation();
    }

    void AudioAnimacionCaminar()
    {
        // Debug.Log("AnimEventHandler.cs - AudioAnimacionCaminar() - INICIO.");
        transform.parent.GetComponent<AudioPasos>().AudioWalkingAnimation();
    }

    void AudioAnimacionCorrer()
    {
        // Debug.Log("AnimEventHandler.cs - AudioAnimacionCorrer() - INICIO.");
        transform.parent.GetComponent<AudioPasos>().AudioRunningAnimation();
    }

    void AudioAnimacionSaltar()
    {
        // Debug.Log("AnimEventHandler.cs - AudioAnimacionSaltar() - INICIO.");
        transform.parent.GetComponent<AudioPasos>().AudioJumpingAnimation();
    }

    void AudioAnimacionAterrizar()
    {
        // Debug.Log("AnimEventHandler.cs - AudioAnimacionAterrizar() - INICIO.");
        transform.parent.GetComponent<AudioPasos>().AudioLandingAnimation();
    }

    void AudioAnimacionReposo()
    {
        // Debug.Log("AnimEventHandler.cs - AudioAnimacionReposo() - INICIO.");
        transform.parent.GetComponent<AudioPasos>().AudioIdleAnimation();
    }
}
