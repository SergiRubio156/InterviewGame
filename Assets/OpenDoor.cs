using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private TransitionCamera transitionCamera;

    public bool lvlComplete;
    public string namePanel;
    [SerializeField]
    Animator doorAnimator;
    public float durationAnimator;
    public AudioSource audioSource;
    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        transitionCamera = GameObject.Find(namePanel).GetComponent<TransitionCamera>();
        lvlComplete = transitionCamera.lvlComplete;
        doorAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //audioSource.Play();
                doorAnimator.SetBool("Close", false);
                doorAnimator.SetBool("Open", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //audioSource.Play();
                doorAnimator.SetBool("Open", false);
                doorAnimator.SetBool("Close", true);
            }
        }
    }
}
