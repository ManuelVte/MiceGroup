using UnityEngine;

public class CheeseBehaviour : MonoBehaviour
{
    public int score = -1;
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    private HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        healthBar = FindAnyObjectByType<HealthBar>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Debug.Log(score);
            meshRenderer.enabled = false;
            sphereCollider.enabled = false;
            sphereCollider.isTrigger = false;
            Destroy(gameObject,2f);
            healthBar.UpdateScore();
        }
    }
}
