using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCamera : MonoBehaviour
{
    public GameObject triggerObject;
    bool StartCameraZoom;
    public GameObject cam1;
    public GameObject cam2;
    public float transitionTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCameraZoom = false;
    }

    public void transitionScene()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(transitionTime);

        GameManager.Instance.UpdateGameState(GameState.Lasers);
    }

}
