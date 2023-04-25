using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPortal : MonoBehaviour
{
    public GameObject animObject;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip audioClip;
    bool hasPlayed = false;

    void Start()
    {
        animator = animObject.GetComponent<Animator>();
        audioSource = animObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !hasPlayed)
        {
            animObject.SetActive(true);
            animator.Play("PORTAL");
            hasPlayed = true;
            audioSource.Play();
            StartCoroutine(DisableAnimation());
        }
    }

    IEnumerator DisableAnimation()
    {
        yield return new WaitForSecondsRealtime(1f);
        animObject.SetActive(false);
    }
}
