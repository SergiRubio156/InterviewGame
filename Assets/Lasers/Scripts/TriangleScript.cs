using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    public GameObject[] Sides = new GameObject[3];
    public LineRenderer[] LineSide = new LineRenderer[3];
    RaycastHit hit;
    Vector3 localForward0;
    Vector3 localForward1;
    Vector3 localForward2;

    int layerWalls;
    int layerObjects;
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
    public void CheckPlane(Vector3 _point ,string _name, bool _bool,Material _mat)
    {
        if (_bool)
        {
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

                    }
                    else if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);
                    }
                    if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);

                    }
                    else if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);
                    }
                    break;

                case "2":

                    LineSide[1].SetPosition(0, Vector3.zero);
                    LineSide[1].SetPosition(1, Vector3.zero);

                    if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);

                    }
                    else if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);
                    }
                    if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);

                    }
                    else if (Physics.Raycast(Sides[2].transform.position, localForward2 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[2].SetPosition(0, Sides[2].transform.position);
                        LineSide[2].SetPosition(1, hit.point);
                    }
                    break;

                case "3":

                    LineSide[2].SetPosition(0, Vector3.zero);
                    LineSide[2].SetPosition(0, Vector3.zero);

                    if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);

                    }
                    else if (Physics.Raycast(Sides[1].transform.position, localForward1 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[1].SetPosition(0, Sides[1].transform.position);
                        LineSide[1].SetPosition(1, hit.point);
                    }

                    if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerObjects))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);

                    }
                    else if (Physics.Raycast(Sides[0].transform.position, localForward0 * 10, out hit, Mathf.Infinity, layerWalls))
                    {
                        LineSide[0].SetPosition(0, Sides[0].transform.position);
                        LineSide[0].SetPosition(1, hit.point);
                    }
                    break;

            }
        }
        else if(!_bool)
        {
            LineSide[0].SetPosition(0, Vector3.zero);
            LineSide[0].SetPosition(1, Vector3.zero);

            LineSide[1].SetPosition(0, Vector3.zero);
            LineSide[1].SetPosition(1, Vector3.zero);

            LineSide[2].SetPosition(0, Vector3.zero);
            LineSide[2].SetPosition(0, Vector3.zero);

        }
    }
}