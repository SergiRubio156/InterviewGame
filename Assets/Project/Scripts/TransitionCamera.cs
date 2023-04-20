using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCamera : MonoBehaviour
{
    public GameObject triggerObject;
    bool StartCameraZoom;
    Camera cam;
    public Animator animaror;

    // Start is called before the first frame update
    void Start()
    {
        StartCameraZoom = false;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerObject != null && Input.GetMouseButtonDown(0) && !StartCameraZoom)
        {
            StartCameraZoom = true;
        }
        if (StartCameraZoom)
        {
            cam.fieldOfView -= Time.deltaTime * 400;
            if (cam.fieldOfView < 1)
            {
                animaror.Play("Fadein");
                SceneManager.LoadScene("Nivel 1");
 
            }
        }
    }
}
