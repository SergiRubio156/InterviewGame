using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColors : MonoBehaviour
{
    public bool red, blue, yellow;
    public int layerWalls;
    public int layerObjects;

    RaycastHit hit;
    public LineRenderer inputLine;
    Vector3 reflectiveRayPoint;

    public Material[] mat = new Material[3];

    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layerWalls = 1 << 9;
        layerObjects = 1 << 7;
    }



    public void RecivedColors(int _name, bool _bool)
    {
        switch (_name)
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
        if (red && blue && yellow)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerObjects))
            {
                Vector3 _hitPoint = hit.point;

                inputLine.SetPosition(0, transform.position);
                inputLine.SetPosition(1, _hitPoint);

                if (hit.transform.gameObject.name == "CubeFinal")
                {
                    hit.transform.gameObject.GetComponent<CheckLaser>().CheckLasers();
                }
            }
            else if (Physics.Raycast(transform.position, transform.forward, out hit, 100, layerWalls))
            {
                Vector3 _hitPoint = hit.point;

                inputLine.SetPosition(0, transform.position);
                inputLine.SetPosition(1, _hitPoint);
            }
        }
        else
        {

            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }
    }
}
