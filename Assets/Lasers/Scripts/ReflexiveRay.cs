using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    int layerObjects;
    int layerWalls;
    bool checkColor = false;
    public GameObject cubeColor;


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

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint, bool _bool,Material mat)
    {
        if (_bool)
        {

            if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerObjects))
            {
                inputLine.material = mat;
                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);

                if (hit.transform.gameObject.name == "Cylinder")
                {
                    checkColor = true;
                    cubeColor = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }

            }
            else if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerWalls))
            {
                inputLine.material = mat;
                if (checkColor == true)
                {
                    checkColor = false;
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }


                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);
            }
        }
        else if(!_bool)
        {
            Debug.Log("entra");
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }
}
