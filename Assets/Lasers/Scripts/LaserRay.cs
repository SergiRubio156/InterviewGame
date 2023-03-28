using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRay : MonoBehaviour
{
    int layerWalls,layerMirror,layerCylinder,layerTriangle, LayerStart;
    public LineRenderer inputLine;

    RaycastHit hit;
    Vector3 reflectiveRayPoint;

    //GameObjects
     public GameObject cubeColor = null;
     public GameObject reflexive = null;
     public GameObject triangle = null;

    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;

        LaserDraw();
    }

    void LaserMirror()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMirror))
        {

            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, _hitPoint);

            if(reflexive != hit.transform.gameObject && reflexive != null)
            {
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position);
                reflexive = null;
            }
            reflexive = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, true, inputLine.material.color, transform.position);

            laserReset("Mirror");
        }
    }

    void LaserColor()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerCylinder))
        {
            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);


            cubeColor = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, true);

            laserReset("Color");
        }
    }

    void LaserDivide()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerTriangle))
        {
            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);


            triangle = hit.transform.gameObject;
            hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, hit.transform.gameObject.name, true, inputLine.material.color);

            laserReset("Divide");
        }

    }

    void LaserStart()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerStart))
        {
            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }

    void LaserWall()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerWalls))
        {
            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }

    void LaserDraw()
    {
            switch (SearchLaser())
            {
                case 6:
                    LaserMirror();
                    break;
                case 7:
                    LaserColor();
                    break;
                case 8:
                    LaserDivide();
                    break;
                case 9:
                    LaserWall();
                    break;
                case 10:
                    LaserStart();
                    break;
                default:
                    LaserWall();
                    break;
            }

    }

    int SearchLaser()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }
    void laserReset(string _name)
    {
        switch(_name)
        {
            case "Mirror":
                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;

                break;
            case "Color":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color,transform.position);
                reflexive = null;
                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;

                break;
            case "Divide":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                break;
            case "all":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;
                break;
        }
    }

}
