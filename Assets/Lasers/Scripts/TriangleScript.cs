using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    public GameObject[] Sides = new GameObject[3];
    public LineRenderer[] LineSide = new LineRenderer[3];
    public GameObject reflexive = null;
    public GameObject cubeColor = null;

    RaycastHit hit;
    Vector3 localForward0;
    Vector3 localForward1;
    Vector3 localForward2;

    int layerWalls;
    int layerObjects;

    bool checkColor;
    bool checkMirror0, checkMirror1, checkMirror2;
    bool checkTriangle = true;
    // Start is called before the first frame update
    void Start()
    {
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        layerWalls = 1 << 9;
        Render();
        layerObjects = 1 << 7;
    }
    void Render()
    {
        localForward0 = Sides[0].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward1 = Sides[1].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward2 = Sides[2].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
    }


    void ReflexTriangle(Vector3 _hitPoint,Vector3 _position,bool _bool, Material _mat)
    {

        Vector3 reflectiveRayPoint = Vector3.Reflect(_hitPoint - _position, hit.normal);

        reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, _bool, _mat);


    }

    public void CheckPlane(Vector3 _point ,string _name, bool _bool,Material _mat)
    {
        if (_bool)
        {
            checkTriangle = false;
            LineSide[0].material = _mat;
            LineSide[1].material = _mat;
            LineSide[2].material = _mat;
            switch (_name)
            {
                case "1":

                    LineSide[0].SetPosition(0, Vector3.zero);
                    LineSide[0].SetPosition(1, Vector3.zero);

                    if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror1 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[1].transform.position, checkMirror1, LineSide[1].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            //hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[1].material.name, checkColor, LineSide[1].material);
                        }

                    }
                    else if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);

                        if (checkMirror1)
                        {
                            checkMirror1 = false;
                            ReflexTriangle(hit.point, Sides[1].transform.position, checkMirror1, LineSide[1].material);
                            reflexive = null;
                        }

                    }
                    if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror2 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[2].transform.position, checkMirror2, LineSide[2].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            //hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[2].material.name, checkColor, LineSide[2].material);
                        }
                    }
                    else if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);

                        if (checkMirror2)
                        {
                            checkMirror2 = false;
                            ReflexTriangle(hit.point, Sides[2].transform.position, checkMirror2, LineSide[2].material);
                            reflexive = null;
                        }
                    }
                    break;

                case "2":

                    LineSide[1].SetPosition(0, Vector3.zero);
                    LineSide[1].SetPosition(1, Vector3.zero);

                    if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror0 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[0].transform.position, checkMirror0, LineSide[0].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            //hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[0].material.name, checkColor, LineSide[0].material);
                        }
                    }
                    else if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);

                        if (checkMirror0)
                        {
                            checkMirror0 = false;
                            ReflexTriangle(hit.point, Sides[0].transform.position, checkMirror0, LineSide[0].material);
                            reflexive = null;
                        }
                    }

                    if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror2 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[2].transform.position, checkMirror2, LineSide[2].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            // hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[2].material.name, checkColor, LineSide[2].material);
                        }
                    }
                    else if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);

                        if (checkMirror2)
                        {
                            checkMirror2 = false;
                            ReflexTriangle(hit.point, Sides[2].transform.position, checkMirror0, LineSide[2].material);
                            reflexive = null;
                        }
                    }
                    break;

                case "3":

                    LineSide[2].SetPosition(0, Vector3.zero);
                    LineSide[2].SetPosition(1, Vector3.zero);

                    if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror0 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[0].transform.position, checkMirror0, LineSide[0].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            //hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[0].material.name, checkColor, LineSide[0].material);
                        }
                    }
                    else if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);

                        if (checkMirror0)
                        {
                            checkMirror0 = false;
                            ReflexTriangle(hit.point, Sides[0].transform.position, checkMirror0, LineSide[0].material);
                            reflexive = null;
                        }
                    }
                    if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);
                        if (hit.transform.gameObject.name == "Mirror")
                        {
                            checkMirror1 = true;
                            reflexive = hit.transform.gameObject;
                            ReflexTriangle(hit.point, Sides[1].transform.position, checkMirror1, LineSide[1].material);
                        }

                        if (hit.transform.gameObject.name == "Cylinder")
                        {
                            checkColor = true;
                            cubeColor = hit.transform.gameObject;
                            //hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(LineSide[1].material.name, checkColor, LineSide[1].material);
                        }
                    }
                    else if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);
                        if (checkMirror1)
                        {
                            checkMirror1 = false;
                            ReflexTriangle(hit.point, Sides[1].transform.position, checkMirror1, LineSide[1].material);
                            reflexive = null;
                        }
                    }
                    break;

            }
        }
        else if(!_bool)
        {
            checkTriangle = true;
            LineSide[0].SetPosition(0, Vector3.zero);
            LineSide[0].SetPosition(1, Vector3.zero);

            LineSide[1].SetPosition(0, Vector3.zero);
            LineSide[1].SetPosition(1, Vector3.zero);

            LineSide[2].SetPosition(0, Vector3.zero);
            LineSide[2].SetPosition(1, Vector3.zero);

        }

    }
}