using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{

    public LineRenderer inputLine;

    RaycastHit hit;
    Vector3 inputRayPoint = new Vector3(0,0,1000);
    Vector3 reflectiveRayPoint;

    void Start()
    {
        inputLine.SetPosition(0, transform.position);
        inputLine.SetPosition(1, inputRayPoint);
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        DrawReflection();
    }

    void DrawReflection()
    {
        if(Physics.Raycast (transform.position, transform.forward, out hit, 100))
        {
            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, _hitPoint);

            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint);
        }
    }

}