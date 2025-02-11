using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Script que contiene el comportamiento de movimiento de la trampa de lanzas.
public class SpearsBehaviour : MonoBehaviour
{
    [SerializeField] private float waitingTimeGoingUp;
    [SerializeField] private float waitingTimeGoingDown;
    [SerializeField] private float goingUpSpeed;
    [SerializeField] private float goingDownSpeed;
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private AudioSource spearsAudioSource;
    [SerializeField] private AudioClip goingUpClip;
    [SerializeField] private AudioClip goingDownClip;

    private bool hasPlayedSound = false;
    private bool previousMovingDown = false;
    private Vector3 targetPosition;
    private bool movingDown;
    private bool isWaiting;
    public AN_HeroController heroController;

    private void Start()
    {
        targetPosition = downPoint.position;
        heroController = FindAnyObjectByType<AN_HeroController>();
    }

    private void Update()
    {
        if (!isWaiting)
        {
            float step = (movingDown ? goingDownSpeed : goingUpSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                if (movingDown)
                {
                    StartCoroutine(WaitBeforMovingUp());
                }
                else
                {
                    StartCoroutine(WaitBeforMovingDown());
                }
            }
        }
    }

    public void SwitchAndPlaySound()
    {
        if (movingDown != previousMovingDown)
        {
            hasPlayedSound = false;
            previousMovingDown = movingDown;
        }

        if (movingDown)
        {
            spearsAudioSource.clip = goingDownClip;

            if (!hasPlayedSound)
            {
                spearsAudioSource.Play();
                hasPlayedSound = true;
            }
        }
        else
        {
            spearsAudioSource.clip = goingUpClip;

            if (!hasPlayedSound)
            {
                spearsAudioSource.Play();
                hasPlayedSound = true;
            }
        }
    }

    private IEnumerator WaitBeforMovingUp()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitingTimeGoingUp);
        movingDown = false;
        targetPosition = upPoint.position;
        isWaiting = false;
    }

    private IEnumerator WaitBeforMovingDown()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitingTimeGoingDown);
        movingDown = true;
        targetPosition = downPoint.position;
        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (heroController != null)
            {
                Debug.Log("Tocado");
                heroController.LifeLoss(25);
            }
            else
            {
                Debug.Log("HerO cONTROLLER ES NOULLK");
            }
        }
    }
}