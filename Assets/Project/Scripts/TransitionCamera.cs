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

    Animator doorAnimator;

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
    }

    void CheckDoor()
    {

    }

    public void transitionScene(string _name)
    {
        if (!lvlComplete)
        {
            cam1.enabled = false;
            cam2.enabled = true;

            lvlComplete = true;

            if (_name == "Door")
                StartCoroutine(WaitDoor());
        }
    }
    public void returntransitionScene()
    {
        cam2.enabled = false;
        cam1.enabled = true;

    }
    IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(transitionTimeDoor);
        GameManager.Instance.State = GameState.Lasers;
    }
}
