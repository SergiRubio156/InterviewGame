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
    public float transitionTime = 3f;
    OutLineObject outLineObject;

    // Start is called before the first frame update
    void Start()
    {
        outLineObject = GetComponent<OutLineObject>();
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

        Debug.Log("!");
        GameManager.Instance.UpdateGameState(GameState.Lasers);
    }

}
