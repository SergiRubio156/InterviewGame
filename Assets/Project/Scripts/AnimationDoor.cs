using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoor : MonoBehaviour
{
    public Animator animator;
    public float durationAnimator;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        audioSource.clip = audioClip;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.OpenDoor())
            {
                audioSource.Play();
                animator.SetTrigger("DoorOpen");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.OpenDoor())
            {
                audioSource.Play();
                animator.SetTrigger("DoorClose");
            }
        }
    }
}