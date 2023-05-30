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

    public bool lvlComplete = false;

    [SerializeField]
    private string nameDoor;
  
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
        nameDoor = transform.parent.gameObject.name;
        CheckDoor();
    }
    private void GameManager_OnGameStateChanged(GameState state)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        cam2 = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void CheckDoor()
    {
        lvlComplete = GameManager.Instance.OpenDoor(nameDoor);
    }

    public void transitionScene(string _name)
    {
        if (!lvlComplete)
        {
            cam1.enabled = false;
            cam2.enabled = true;


            if (_name == "Door")
            {
                CheckDoor();
                StartCoroutine(WaitDoor());
            }
        }

    }
    IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(transitionTimeDoor);
        GameManager.Instance.State = GameState.Lasers;
    }


}