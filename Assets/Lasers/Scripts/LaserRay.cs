using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{
    int layerWalls;
    int layerObjects;
    public LineRenderer inputLine;

    //Booleans
    bool checkColor = false;
    bool checkMirror = false;
    bool checkTriangle = false;

    RaycastHit hit;
    Vector3 reflectiveRayPoint;

    //GameObjects
    public GameObject cubeColor = null;
    public GameObject reflexive = null;
    public GameObject triangle = null;

    int materialNum = 1;
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
        InputColor();
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        layerWalls = 1 << 7;
        layerWalls = ~layerWalls;

        layerObjects = 1 << 7;

        DrawReflection();
    }

    void InputColor()
    {
        if (inputLine.material.name == "Red (Instance)")
            materialNum = 0;
        else if (inputLine.material.name == "Blue (Instance)")
            materialNum = 1;
        else if (inputLine.material.name == "Yellow (Instance)")
            materialNum = 2;
    }
    void DrawReflection()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerObjects))
        {
            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, _hitPoint);

            if (hit.transform.gameObject.name == "Mirror")
            {
                checkMirror = true;
                reflexive = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, checkMirror, inputLine.material);
                if (checkColor)
                {
                    checkColor = false;
                    cubeColor.GetComponent<CubeColors>().RecivedColors(materialNum, checkColor);
                }
            }
            else if (checkMirror)
            {
                checkMirror = false;
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(hit.point, reflectiveRayPoint, checkMirror, inputLine.material);
            }

            if ((hit.transform.gameObject.name == "1") || (hit.transform.gameObject.name == "2") || (hit.transform.gameObject.name == "3"))
            {
                checkTriangle = true;
                triangle = hit.transform.gameObject;
                hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, hit.transform.gameObject.name, checkTriangle, inputLine.material);

            }

            if (hit.transform.gameObject.name == "Cylinder")
            {
                checkColor = true;
                cubeColor = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(materialNum, checkColor);
                if (checkMirror)
                {
                    checkMirror = false;
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(hit.point, reflectiveRayPoint, checkMirror, inputLine.material);
                }
            }
            else if (checkColor)
            {
                checkColor = false;
                cubeColor.GetComponent<CubeColors>().RecivedColors(materialNum, checkColor);
            }
        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, 100, layerWalls))
        {
            if (checkColor)
            {
                checkColor = false;
                cubeColor.GetComponent<CubeColors>().RecivedColors(materialNum, checkColor);
                cubeColor = null;
            }
            if(checkMirror)
            {
                checkMirror = false;
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(hit.point, reflectiveRayPoint, checkMirror, inputLine.material);
                reflexive = null;
            }
            if (checkTriangle)
            {
                checkTriangle = false;
                triangle.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, triangle.name, checkMirror, inputLine.material);
                triangle = null;
            }

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);
        }
    }



}