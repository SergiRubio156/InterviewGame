using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDoor : MonoBehaviour
{
    public GameObject animObject;
    public Animator animator;
    bool hasPlayed = false;

    void Start()
    {
        animator = animObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !hasPlayed)
        {
            animObject.SetActive(true);
            animator.Play("door");
            hasPlayed = true;
            StartCoroutine(DisableAnimation());
        }
    }

    IEnumerator DisableAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0) .length);
        animObject.SetActive(false);
    }

}