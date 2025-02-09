using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [Tooltip("True - red key object, false - blue key")]
    public bool isRedKey = true;
    [SerializeField] private AudioClip keySound;
    //PlayerDynamics character;
    HeroInteractive character;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    private void Start()
    {
        character = FindAnyObjectByType<HeroInteractive>(); // key will get up and it will saved in "inventary"


        // FindObjectOfType<HeroInteractive>();
    }

    void Update()
    {
        if ( NearView() && Input.GetKeyDown(KeyCode.E) )
        {
            if (isRedKey) character.RedKey = true;
            else character.BlueKey = true;
            AudioSource.PlayClipAtPoint(keySound, transform.position);
            Destroy(gameObject);
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 2f) return true; // angleView < 35f && 
        else return false;
    }
}
