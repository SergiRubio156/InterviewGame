using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCamera : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public float transitionTimeDoor = 1f;
    public float transitionTimeSpawn = 3f;

    OutLineObject outLineObject;
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
        cam2 = GetComponentInChildren<GameObject>().gameObject;
    }

    public void transitionScene(string _name)
    {
        cam1.SetActive(false);
        cam2.SetActive(true);

        Debug.Log("!");
        if(_name == "Door")
            StartCoroutine(WaitDoor());
    }
    public void returntransitionScene()
    {
        cam2.SetActive(false);
        cam1.SetActive(true);

    }
    IEnumerator WaitDoor()
    {
        yield return new WaitForSeconds(transitionTimeDoor);
        GameManager.Instance.State = GameState.Lasers;
    }
}
