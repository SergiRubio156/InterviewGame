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
    CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        StartCameraZoom = false;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerObject != null && Input.GetMouseButtonDown(0) && !StartCameraZoom)
        {
            //StartCameraZoom = true;
        }
        if (StartCameraZoom)
        {
            cam.m_Lens.FieldOfView -= Time.deltaTime * 350;
            if (cam.m_Lens.FieldOfView < 1)
            {
                //SceneManager.LoadScene("Nivel 1");
 
            }
        }
    }
}
