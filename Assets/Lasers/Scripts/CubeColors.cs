using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColors : MonoBehaviour
{
    public bool red, blue, yellow;

    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    RaycastHit hit;
    public GameObject sun;
    LineRenderer inputLine;
    Vector3 reflectiveRayPoint;

    public Material[] nameColor = new Material[4];
    public MeshRenderer[] renderer = new MeshRenderer[3];

    bool checkMirror;

    public GameObject cubeColor = null;
    public GameObject reflexiveCube = null;
    public GameObject triangle = null;
    public GameObject laserFinal = null;

    // Start is called before the first frame update
    void Start()
    {
        inputLine = sun.GetComponent<LineRenderer>();
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

        if (ConfirmLine())
        {
            laserReset("all");
        }
    }

    public bool ConfirmLine()
    {
        if (inputLine.GetPosition(0) == Vector3.zero)
            return true;
        return false;
    }

    void LaserMirror()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMirror))
        {

            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, sun.transform.position);
            inputLine.SetPosition(1, _hitPoint);

            /*if (reflexiveCube.name != hit.transform.gameObject.name && reflexiveCube != null)
            {
                reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position);
                reflexiveCube = null;
            }*/

            reflexiveCube = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, true, inputLine.material.color, transform.position,gameObject);

            laserReset("Mirror");
        }
    }

    void LaserDivide()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerTriangle))
        {
            inputLine.SetPosition(0, sun.transform.position);
            inputLine.SetPosition(1, hit.point);

            triangle = hit.transform.gameObject;
            string _name = hit.transform.gameObject.name;

            hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, true, inputLine.material,gameObject);

                laserReset("Divide");
        }

    }

    void LaserStart()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerStart))
        {
            inputLine.SetPosition(0, sun.transform.position);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }

    void LaserWall()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerWalls))
        {
            inputLine.SetPosition(0, sun.transform.position);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");
        }
    }
    void LaserFinal()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerFinal))
        {
            inputLine.SetPosition(0, sun.transform.position);
            inputLine.SetPosition(1, hit.point);


            laserFinal = hit.transform.gameObject;
            Vector3 direccion = hit.point - transform.position;
            hit.transform.gameObject.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material.color, direccion);
            laserReset("Final");
        }

    }
    public void RecivedColors(Material _mat, bool _bool,string _name)
    {
        Color _color = _mat.color;
        if (!red || !blue || !yellow || !_bool)
        {
            for (int i = 0; i < nameColor.Length; i++)
            {
                if (nameColor[i].color == _color)
                {
                    switch (i)
                    {
                        case 0:
                            red = _bool;
                            RenderColor(_name, _color, _bool,"Red");
                            break;
                        case 1:
                            blue = _bool;
                            RenderColor(_name, _color, _bool,"Blue");
                            break;
                        case 2:
                            yellow = _bool;
                            RenderColor(_name, _color, _bool,"Yellow");
                            break;
                    }
                }
            }
            if (renderer[3].material != nameColor[3])
                RenderColor("5", _color, false, "Sun");
        }
        if (red && blue && yellow)
        {
            RenderColor("4", _color, _bool,"Yellow");
            LaserDraw();
        }
        else
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }

    void RenderColor(string _name,Color _color,bool _bool,string _name2)
    {
        switch (_name)
        {
            case "1":
                if (_bool)
                    renderer[0].material.color = _color;
                break;
            case "2":
                if (_bool)
                    renderer[1].material.color = _color;
                break;
            case "3":
                if(_bool)
                    renderer[2].material.color = _color;
                break;
            case "4":
                if(_bool)
                    renderer[3].material = nameColor[4];
                break;
            default:
                if (_name2 == "Red")
                    forColor(_color);
                if (_name2 == "Blue")
                    forColor(_color);
                if (_name2 == "Yellow")
                    forColor(_color);
                if (_name2 == "Sun")
                    renderer[3].material = nameColor[3];
                break;
        }
    }

    void forColor(Color _name)
    {
        for(int i = 0; i < renderer.Length; i++)
        {
            if(renderer[i].material.color == _name)
                renderer[i].material = nameColor[3];
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
            case 8: //TRIANGLE
                LaserDivide();
                break;
            case 9: //WALL
                LaserWall();
                break;
            case 10: //LaserStart
                LaserStart();
                break;
            case 11: //LaserFinal
                LaserFinal();
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
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false,_name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Color":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero,null);
                reflexiveCube = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Divide":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Final":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;
                break;

            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;
        }
    }
}
