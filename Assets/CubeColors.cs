using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColors : MonoBehaviour
{
    public bool red, blue, yellow;

    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart;

    RaycastHit hit;
    public LineRenderer inputLine;
    Vector3 reflectiveRayPoint;

    public Material[] nameColor = new Material[4];

    bool checkMirror;

    public GameObject cubeColor = null;
    public GameObject reflexiveCube = null;
    public GameObject triangle = null;
    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;

    }

    void LaserMirror()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMirror))
        {

            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, _hitPoint);

            /*if (reflexiveCube.name != hit.transform.gameObject.name && reflexiveCube != null)
            {
                reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position);
                reflexiveCube = null;
            }*/

            reflexiveCube = hit.transform.gameObject;
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


            if (cubeColor.name != hit.transform.gameObject.name)
            {
                cubeColor = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, true);

                laserReset("Color");
            }
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

    bool LaserWall()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerWalls))
        {
            inputLine.SetPosition(0, transform.position);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");
            return true;
        }
        return false;
    }

    public void RecivedColors(Color _name, bool _bool)
    {
        if (nameColor[3].color != _name)
        {
            for (int i = 0; i < nameColor.Length-1; i++)
            {
                if (nameColor[i].color == _name)
                {
                    switch (i)
                    {
                        case 0:
                            red = _bool;
                            break;
                        case 1:
                            blue = _bool;
                            break;
                        case 2:
                            yellow = _bool;
                            break;
                    }
                }
            }
            if (red && blue && yellow)
            {
                LaserDraw();
            }
            else
            {
                inputLine.SetPosition(0, Vector3.zero);
                inputLine.SetPosition(1, Vector3.zero);
            }
        }
    }
    int SearchLaser()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }
    void LaserDraw()
    {
            switch (SearchLaser())
            {
                case 6: //MIRROR
                    LaserMirror();
                    break;
                case 7: //CYLINDER
                    LaserColor();
                    break;
                case 8: //TRIANGLE
                    LaserDivide();
                    break;
                case 9: //WALL
                    LaserWall();
                    break;
                case 10: //LaserStart
                    LaserStart();
                    break;
                default:
                    break;
            }
    }

    void laserReset(string _name)
    {
        switch (_name)
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
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;

                break;
            case "Divide":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                break;
            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

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
