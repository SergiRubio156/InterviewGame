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
    bool checkMirror = false;
    public GameObject cubeColor;
    public GameObject reflexiveCube;
    Vector3 reflectiveRayPoint2;


    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layerWalls = 1 << 7;
        layerWalls = ~layerWalls;
        layerObjects = 1 << 7;
    }

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint, bool _bool,Material mat)
    {
       
        if (_bool)
        {

            if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerObjects))
            {
                //POSICION Y MATERIAL DEL LINE RENDERER
                inputLine.material = mat;
                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);


                //SI EL MIRROR XOCA CON UN CYLINDER
                if (hit.transform.gameObject.name == "Cylinder")
                {
                    checkColor = true;
                    cubeColor = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }

                //SI EL MIRROR XOCA CON UN MIRROR
                if (hit.transform.gameObject.name == "Mirror")
                {

                    reflectiveRayPoint2 = Vector3.Reflect(hit.point - point, hit.normal);

                    checkMirror = true;
                    reflexiveCube = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<ReflexiveRay>().ReflexiveMirror(hit.point, reflectiveRayPoint2, checkMirror, inputLine.material);
                }



            }
            else if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerWalls))
            {
                //POSICION Y MATERIAL DEL LINE RENDERER
                inputLine.material = mat;
                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);

                //SI EL MIRROR NO XOCA CON UN MIRROR
                if (checkMirror)
                {
                    checkMirror = false;
                    reflectiveRayPoint2 = Vector3.Reflect(hit.point - point, hit.normal);
                    reflexiveCube.GetComponent<ReflexiveRay>().ReflexiveMirror(hit.point, reflectiveRayPoint2, checkMirror, inputLine.material);
                }

                //SI EL MIRROR NO XOCA CON UN CYLINDER
                if (checkColor)
                {
                    checkColor = false;
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }

            }
        }//RESETEAMOS EL LINE RENDERER A 0
        else if(!_bool)
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }
    }

    public void ReflexiveMirror(Vector3 point, Vector3 reflectiveRayPoint, bool _bool, Material mat)
    {
        if (_bool) //ESTA FUNCION SOLO SE ENTRA SI UN MIRROR XOCA CON OTRO MIRROR
        {
            if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerObjects))
            {
                //POSICION Y MATERIAL DEL LINE RENDERER
                inputLine.material = mat;
                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);

                //SI EL MIRROR XOCA CON UN CYLINDER
                if (hit.transform.gameObject.name == "Cylinder")
                {
                    checkColor = true;
                    cubeColor = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }


            }
            else if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerWalls))
            {
                //POSICION Y MATERIAL DEL LINE RENDERER
                inputLine.material = mat;
                inputLine.SetPosition(0, point);
                inputLine.SetPosition(1, hit.point);

                //SI EL MIRROR NO XOCA CON UN CYLINDER
                if (checkColor)
                {
                    checkColor = false;
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.name, checkColor);
                }
                if (checkMirror)
                {
                    checkMirror = false;
                    reflectiveRayPoint2 = Vector3.Reflect(hit.point - point, hit.normal);
                    reflexiveCube.GetComponent<ReflexiveRay>().ReflexiveMirror(hit.point, reflectiveRayPoint2, checkMirror, inputLine.material);
                    reflexiveCube = null;
                }
            }
        }
        else if (!_bool)//RESETEAMOS EL LINE RENDERER QUE XOCA CON EL MIRROR A 0
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }
    }
}
