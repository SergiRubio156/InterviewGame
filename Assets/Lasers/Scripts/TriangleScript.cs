using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    public GameObject[] Sides = new GameObject[3];
    public LineRenderer[] inputLine = new LineRenderer[3];
    Vector3[] localForward = new Vector3[3];

    public GameObject[] reflexive = new GameObject[3];
    public GameObject[] cubeColor = new GameObject[3];
    public GameObject[] triangle = new GameObject[3];
    public GameObject[] laserFinal = new GameObject[3];
    public Material[] mat = new Material[4];

    public GameObject objectRecvied;

    RaycastHit hit;

    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    Vector3 point;
    public int nameSide = 0;

    public bool checkBool;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Sides.Length; i++)
        {
            reflexive[i] = null;
            cubeColor[i] = null;
            triangle[i] = null;
            laserFinal[i] = null;
        }
        Render();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;

        Render();
        if (ConfirmLine(nameSide) && !checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                laserReset("all", i);
            }
        }
    }

    public bool ConfirmLine(int _name)
    {
        if (inputLine[nameSide].GetPosition(1) == Vector3.zero)
            return true;
        return false;
    }
    void Render()
    {
        for(int i = 0; i < localForward.Length; i++)
        {
            localForward[i] = Sides[i].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        }
    }


    void LaserMirror(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity, layerMirror))
        {
            
            Vector3 _hitPoint = hit.point;
            Vector3 reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, _hitPoint);


            reflexive[i] = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, true, inputLine[i].material.color, transform.position,gameObject);

            laserReset("Mirror", i);

        }
    }

    void LaserColor(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity, layerCylinder))
        {
            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, hit.point);


            cubeColor[i] = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine[i].material.color, true);

            laserReset("Color", i);
        }
    }

    void LaserDivide(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity, layerTriangle))
        {
            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, hit.point);


            triangle[i] = hit.transform.gameObject;
            hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, hit.transform.gameObject.name, true, inputLine[i].material.color,gameObject);

            laserReset("Divide", i);
        }

    }

    void LaserStart(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity, LayerStart))
        {
            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, hit.point);

            laserReset("all", i);

        }

    }

    void LaserWall(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity, layerWalls))
        {
            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, hit.point);

            laserReset("all", i);

        }

    }
    void LaserFinal(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, 100, LayerFinal))
        {
            inputLine[i].SetPosition(0, transform.position);
            inputLine[i].SetPosition(1, hit.point);


            laserFinal[i] = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine[i].material.color);

            laserReset("Final", i);
        }

    }
    void LaserDraw(int _name)
    {
        if (checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                if (i != _name)
                {
                    switch (SearchLaser(i))
                    {
                        case 6:
                            LaserMirror(i);
                            break;
                        case 7:
                            LaserColor(i);
                            break;
                        case 8:
                            LaserDivide(i);
                            break;
                        case 9:
                            LaserWall(i);
                            break;
                        case 10:
                            LaserStart(i);
                            break;
                        case 11:
                            LaserFinal(i);
                            break;
                        default:
                            LaserWall(i);
                            break;
                    }
                }
                else if (i == _name)
                {
                    inputLine[i].SetPosition(0, Vector3.zero);
                    inputLine[i].SetPosition(1, Vector3.zero);

                }
            }
        }
        else if(!checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                inputLine[i].SetPosition(0, Vector3.zero);
                inputLine[i].SetPosition(1, Vector3.zero);
            }

        }

    }

    int SearchLaser(int i)
    {
        if (Physics.Raycast(Sides[i].transform.position, localForward[i], out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }

    public void CheckPlane(Vector3 _point, string _name, bool _bool, Color _color, GameObject _gameObject)
    {
        if (_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            if (_point == Vector3.zero)
            {
                checkBool = false;
            }
            if (_name == "1")
                nameSide = 0;
            else if (_name == "2")
                nameSide = 1;
            else if (_name == "3")
                nameSide = 2;

            checkBool = _bool;
            TakeColor(_color);

            LaserDraw(nameSide);
        }
    }

    void TakeColor(Color _color)
    {
        for (int i = 0; i < mat.Length; i++)
        {
            if (mat[i].color == _color)
            {
                inputLine[0].material = mat[i];
                inputLine[1].material = mat[i];
                inputLine[2].material = mat[i];

            }
        }

    }
    void laserReset(string _name, int i)
    {
        switch (_name)
        {
            case "Mirror":
                if (cubeColor[i] != null)
                    cubeColor[i].GetComponent<CubeColors>().RecivedColors(inputLine[i].material.color, false);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material.color,null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material.color);
                laserFinal[i] = null;
                break;
            case "Color":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material.color, transform.position, null);
                reflexive[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material.color, null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material.color);
                laserFinal[i] = null;
                break;
            case "Divide":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material.color, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)
                    cubeColor[i].GetComponent<CubeColors>().RecivedColors(inputLine[i].material.color, false);
                cubeColor[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material.color);
                laserFinal[i] = null;
                break;
            case "Final":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material.color, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)

                    cubeColor[i].GetComponent<CubeColors>().RecivedColors(inputLine[i].material.color, false);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material.color, null);
                triangle[i] = null;
                break;
            case "all":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material.color, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)

                    cubeColor[i].GetComponent<CubeColors>().RecivedColors(inputLine[i].material.color, false);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material.color, null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material.color);
                laserFinal[i] = null;
                break;
        }
    }
}