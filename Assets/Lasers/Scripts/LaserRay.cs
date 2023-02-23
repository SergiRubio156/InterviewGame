using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{
    public LineRenderer inputRay;
    public LineRenderer reflectiveRay;

    bool reflectiveRayBool = false;

    RaycastHit hit;
    Vector3 clickedPoint;
    Vector3 inputRayPoint = new Vector3(0,0,1000);
    Vector3 reflectiveRayPoint;



    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            reflectiveRayBool = true;
            reflectiveRay.gameObject.SetActive(reflectiveRayBool);
        }
        else
        {
            reflectiveRayBool = false;
            reflectiveRay.gameObject.SetActive(reflectiveRayBool);
            inputRay.SetPosition(0, transform.position);
            inputRay.SetPosition(1, inputRayPoint);
        }

        RotateBall();
        DrawReflection();

    }

    public void RotateBall()
    {
        Vector3 _rayHitPoint = transform.forward;
        Ray _rayFromScreen = Camera.main.ScreenPointToRay(clickedPoint);

        Physics.Raycast(_rayFromScreen.origin, _rayFromScreen.direction * 100, out hit, 1000);
            _rayHitPoint = hit.point;


        //Quaternion _targetRotation = Quaternion.LookRotation(_rayHitPoint - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime);
    }

    void DrawReflection()
    {
        if(Physics.Raycast (transform.position, transform.forward, out hit, 100) && reflectiveRayBool)
        {
            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputRay.SetPosition(0, transform.position);
            inputRay.SetPosition(1, _hitPoint);

            reflectiveRay.SetPosition(0, _hitPoint);
            reflectiveRay.SetPosition(1, reflectiveRayPoint * 3);
        }
    }

}