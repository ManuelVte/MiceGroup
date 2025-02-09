using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Tooltip("If it is false door can't be used")]
    public bool Locked = false;
    [Tooltip("It is true for remote control only")]
    public bool Remote = false;
    [Space]
    [Tooltip("Door can be opened")]
    public bool CanOpen = true;
    [Tooltip("Door can be closed")]
    public bool CanClose = true;
    [Space]
    [Tooltip("Door locked by red key (use key script to declarate any object as key)")]
    public bool RedLocked = false;
    public bool BlueLocked = false;
    [Tooltip("It is used for key script working")]
    HeroInteractive character;
    [Space]
    public bool isOpened = false;
    [Range(0f, 4f)]
    [Tooltip("Speed for door opening, degrees per sec")]
    public float OpenSpeed = 3f;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    // Hinge
    [HideInInspector]
    public Rigidbody rbDoor;
    HingeJoint hinge;
    JointLimits hingeLim;
    float currentLim;

    // Sound
    private AudioSource audioSourceDoor;
    private Quaternion lastDoorRotation;
    private float countFramesNoRotation = 0;


    void Start()
    {
        audioSourceDoor = GetComponent<AudioSource>();
        lastDoorRotation = transform.rotation;
        rbDoor = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        character = FindAnyObjectByType<HeroInteractive>();
        //FindObjectOfType<AN_HeroInteractive>();

    }

    void Update()
    {
        if (!Remote && Input.GetKeyDown(KeyCode.E) && NearView())
        {
            Action();
            //AudioSource.PlayClipAtPoint(doorOpeningSound, transform.position);
            //Debug.Log("Jugador abre puerta");
        }

        //Debug.Log("Rotación actual puerta: " + transform.rotation + " - Última rotación puerta: " + lastDoorRotation);
        if (transform.rotation != lastDoorRotation) 
        {
            //Debug.Log("SONIDO");
            countFramesNoRotation = 0;

            if (!audioSourceDoor.isPlaying)
            {
                //Debug.Log("Play");
                audioSourceDoor.Play();
            }
        }
        else if (audioSourceDoor.isPlaying)
        {
            countFramesNoRotation += 1 * Time.deltaTime;
            //Debug.Log("Contador: " +  countFramesNoRotation); 

            // Si pasa un cuarto de segundo sin que se haya movido la puerta paramos el sonido
            if (countFramesNoRotation >= 0.25f)
            {
                //Debug.Log("Stop");
                audioSourceDoor.Pause();
            }
        }

        lastDoorRotation = transform.rotation;

    }

    public void Action() // void to open/close door
    {
        if (!Locked)
        {
            // key lock checking
            if (character != null && RedLocked && character.RedKey)
            {
                RedLocked = false;
                character.RedKey = false;
            }
            else if (character != null && BlueLocked && character.BlueKey)
            {
                BlueLocked = false;
                character.BlueKey = false;
            }
            
            // opening/closing
            if (isOpened && CanClose && !RedLocked && !BlueLocked)
            {
                isOpened = false;
            }
            else if (!isOpened && CanOpen && !RedLocked && !BlueLocked)
            {
                isOpened = true;
                rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f)); 
            }
        
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 3f) return true; // angleView < 35f && 
        else return false;
    }

    private void FixedUpdate() // door is physical object
    {
        if (isOpened)
        {
            currentLim = 85f;
        }
        else
        {
            // currentLim = hinge.angle; // door will closed from current opened angle
            if (currentLim > 1f)
                currentLim -= .5f * OpenSpeed;
        }

        // using values to door object
        hingeLim.max = currentLim;
        hingeLim.min = -currentLim;
        hinge.limits = hingeLim;
    }
}
