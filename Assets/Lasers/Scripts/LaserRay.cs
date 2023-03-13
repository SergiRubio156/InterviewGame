using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{

    public LineRenderer inputLine;

    RaycastHit hit;
    Vector3 inputRayPoint = new Vector3(0,0,1000);
    Vector3 reflectiveRayPoint;
    bool checkLaser = false;
    public GameObject reflexive;
    void Start()
    {
        ResetLaser();
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        DrawReflection();
        if (!checkLaser)
            ResetLaser();
    }

    void DrawReflection()
    {
        checkLaser = false;
        if (Physics.Raycast (transform.position, transform.forward, out hit, 100))
        {
            checkLaser = true;
            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, _hitPoint);

            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint);
        }
    }
    void ResetLaser()
    {
        Debug.Log("1");
        reflexive.GetComponent<ReflexiveRay>().Checks(checkLaser);
        inputLine.SetPosition(0, transform.position);
        inputLine.SetPosition(1, inputRayPoint);
    }


}