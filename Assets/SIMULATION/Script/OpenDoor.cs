using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private TransitionCamera transitionCamera;

    public bool lvlComplete;
    [SerializeField]
    Animator doorAnimator;
    public AudioSource audioSource;
    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        transitionCamera = GetComponentInChildren<TransitionCamera>();
        lvlComplete = transitionCamera.lvlComplete;
        doorAnimator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        if (lvlComplete)
        {
            audioSource.enabled = true;
            audioSource.Play();
            doorAnimator.SetBool("OpenDoor", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                doorAnimator.SetBool("OpenDoor", true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                doorAnimator.SetBool("OpenDoor", false);
            }
        }
    }
}
