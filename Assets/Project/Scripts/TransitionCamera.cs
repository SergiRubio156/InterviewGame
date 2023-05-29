using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public float transitionTimeDoor = 0.5f;
    public float transitionTimeSpawn = 3f;

    public bool lvlComplete = false;
    public int doorInt;

    //ANIMATION
    [SerializeField]
    Animator doorAnimator;
    public float durationAnimator;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
    }
    private void GameManager_OnGameStateChanged(GameState state)  
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        cam2 = GetComponentInChildren<CinemachineVirtualCamera>();
        doorAnimator = GetComponentInParent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    void CheckDoor()
    {
        lvlComplete = GameManager.Instance.OpenDoor(doorInt);
    }

    public void transitionScene(string _name)
    {
        if (!lvlComplete)
        {
            cam1.enabled = false;
            cam2.enabled = true;


            if (_name == "Door")
                StartCoroutine(WaitDoor());
        }
        else
        {
            
        }
    }
    IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(transitionTimeDoor);
        GameManager.Instance.State = GameState.Lasers;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
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
                audioSource.Play();
                doorAnimator.SetBool("Close", true);
            }
        }
    }
}
