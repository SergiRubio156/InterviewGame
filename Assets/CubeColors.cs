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



    public void RecivedColors(string _name,bool _bool)
    {
        switch (_name)
        {
            case "Red (Instance)":
                red = _bool;
                break;
            case "Red (Instance) (Instance)":
                red = _bool;
                break;
            case "Red (Instance) (Instance) (Instance)":
                red = _bool;
                break;
            case "Blue (Instance)":
                blue = _bool;
                break;
            case "Blue (Instance) (Instance)":
                blue = _bool;
                break;
            case "Blue (Instance) (Instance) (Instance)":
                blue = _bool;
                break;
            case "Yellow (Instance)":
                yellow = _bool;
                break;
            case "Yellow (Instance) (Instance)":
                yellow = _bool;
                break;
            case "Yellow (Instance) (Instance) (Instance)":
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
