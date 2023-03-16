using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    int layerObjects;
    int layerWalls;
    bool checkColor = false;
    bool checkMirror = false;
    public GameObject cubeColor;
    Vector3 reflectiveRayPoint2;


    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layerWalls = 1 << 9;
        layerObjects = 1 << 7;
    }

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint, bool _bool,Material mat)
    {
       
        if (_bool)
        {

            if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerObjects))
            {
                if (!checkMirror)
                {
                    inputLine.material = mat;
                    inputLine.SetPosition(0, point);
                    inputLine.SetPosition(1, hit.point);
                }
                if (hit.transform.gameObject.name == "Cylinder")
                {
                    checkColor = true;
                    cubeColor = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }
                if (hit.transform.gameObject.name == "Mirror(1)")
                {
                    Vector3 _hitPoint = hit.point;
                    reflectiveRayPoint2 = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

                    Debug.Log(hit.transform.gameObject.name);
                    checkMirror = true;
                    ReflexiveMirror(_hitPoint, reflectiveRayPoint2, checkMirror, inputLine.material);
                }
            }
            else if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerWalls))
            {
                inputLine.material = mat;
                if (checkColor == true)
                {
                    checkColor = false;
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }


                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);
            }
        }
        else if(!_bool)
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }
    }

    void ReflexiveMirror(Vector3 point, Vector3 reflectiveRayPoint, bool _bool, Material mat)
    {
        if (_bool)
        {
            Debug.Log("hola");
            inputLine.material = mat;
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, reflectiveRayPoint2 * 3);
        }
    }
}
