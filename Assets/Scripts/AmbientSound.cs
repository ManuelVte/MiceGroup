using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [Header("Altavoces Maestro-Esclavos")]
    [Tooltip("Agregar aquí el AudioSource Maestro.")]
    [SerializeField] private AudioSource mastro;
    [Tooltip("Agregar tantos AudioSources Esclavos como se desee.")]
    [SerializeField] private AudioSource[] esclavo;

    void Update()
    {
        for (int i = 0; i < esclavo.Length; i++) esclavo[i].timeSamples = mastro.timeSamples;
    }
}
