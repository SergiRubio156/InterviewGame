using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitipnCamera : MonoBehaviour
{
    bool StartCameraZoom;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        StartCameraZoom = false;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) 
        { 
        StartCameraZoom= true;
        }
        if (StartCameraZoom)
        {
            cam.fieldOfView -= Time.deltaTime * 80;
            if (cam.fieldOfView < 1 )
            {
                SceneManager.LoadScene("Nivel 1");

            }
        }
    }
}
