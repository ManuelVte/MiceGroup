using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imagen;
    [SerializeField] private TMP_Text texto;
    private AN_HeroController controller;
    private CheeseBehaviour score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        controller = FindAnyObjectByType<AN_HeroController>();
        score = FindAnyObjectByType<CheeseBehaviour>();
        texto.text = score.score.ToString();
    }

    
    // Update is called once per frame
    public void HealthDrain()
    {
        imagen.fillAmount = controller.life / 100;
    }

    public void UpdateScore()
    {
        if (score != null)
        {
            score.score += 1;
            texto.text = score.score.ToString();
        }
        else
        {
            Debug.Log("Score es NULL");
        }
    }
}
