using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    bool sceneSettings;
    Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnMouseDrag()
    {
        Debug.Log("Entra");
        worldPosition = Input.mousePosition;
        worldPosition.z = 10.0f;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, worldPosition.z);
        curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if (curScreenPoint.y <= 0)
            curScreenPoint.y = 0.02f;
        //ObjectMove.transform.position = curScreenPoint;
    }
}
