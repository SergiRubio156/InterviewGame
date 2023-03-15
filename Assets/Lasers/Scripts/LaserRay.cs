using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{
    public int layerWalls = 1 << 9;
    public int layerObjects = 1 << 7;
    public LineRenderer inputLine;

    //Booleans
    bool checkColor = false;
    bool checkMirror = false;


    RaycastHit hit;
    Vector3 reflectiveRayPoint;

    //GameObjects
    public GameObject cubeColor = null;
    public GameObject reflexive = null;

    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        layerWalls = 1 << 9;
        
        layerObjects = 1 << 7;

        DrawReflection();
    }
    void DrawReflection()
    {
        if (Physics.Raycast (transform.position, transform.forward, out hit, Mathf.Infinity, layerObjects))
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
            }
            //string name = hit.transform.gameObject.name;
            //if (hit.transform.gameObject.name == "1")
                //hit.transform.gameObject.GetComponent<TriangleScript>().CheckPlane(name);
            if (hit.transform.gameObject.name == "Cylinder")
            {
                checkColor = true;
                cubeColor = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
            }
            

        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, 100, layerWalls))
        {
            if (checkColor)
            {
                checkColor = false;
                cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
            }
            if(checkMirror)
            {
                checkMirror = false;
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(hit.point, reflectiveRayPoint, checkMirror, inputLine.material);
            }


            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);
        }
    }



}