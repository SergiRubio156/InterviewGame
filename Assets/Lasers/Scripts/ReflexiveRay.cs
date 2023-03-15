using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    public int layerObjects = 1 << 7;
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
        layerObjects = 1 << 7;
    }

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint, bool _bool,Material mat)
    {
        if (_bool)
        {
            inputLine.material = mat;
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, reflectiveRayPoint * 3);
 
            Debug.DrawRay(point, reflectiveRayPoint * 3 - point, Color.black);
            if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerObjects))
            {
                if (hit.transform.gameObject.name == "Cylinder")
                {
                    checkColor = true;
                    cubeColor = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }
                /*if (hit.transform.gameObject.name == "")
                {

                }*/
            }
            else if (checkColor)
            {
                checkColor = false;
                cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
            }
        }
        else
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }
}
