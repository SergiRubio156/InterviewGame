using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    public GameObject cubeColor = null;
    public GameObject reflexiveCube = null;
    public GameObject triangle = null;
    public GameObject laserFinal = null;

    public Material[] mat = new Material[4];

    public GameObject objectRecvied;

    Vector3 reflectiveRayPoint;
    Vector3 point;
    Vector3 transformStart;
    bool checkBool1;
    bool checkBool = false;
    int nameSide;
    public int num,numRef;
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
        LayerFinal = 1 << 11;

        if (ConfirmLine())
        {
            laserReset("all");
        }
    }

    public bool ConfirmLine()
    {
        if(inputLine.GetPosition(1) == Vector3.zero)
            return true;
        return false;
    }

    int SearchLaser()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }
    void LaserMirror()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, layerMirror))
        {               
            Vector3 _hitPoint = hit.point;
            Vector3 reflectiveRayPoint2 = Vector3.Reflect(hit.point - point, hit.normal);


            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

                if (reflexiveCube != hit.transform.gameObject && reflexiveCube != null)
                {
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position,gameObject);
                    reflexiveCube = null;

                }

                reflexiveCube = hit.transform.gameObject;
            if (this.gameObject.name != hit.transform.gameObject.name)
                hit.transform.gameObject.GetComponent<ReflexiveRay>().ReflexiveMirror(_hitPoint, reflectiveRayPoint2, true, inputLine.material.color, transform.position,gameObject);
                laserReset("Mirror");

        }
    }

    void LaserColor()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, layerCylinder))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

            if (num != 7)
            {
                cubeColor = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, true);

                laserReset("Color");
            }
        }
    }

    void LaserDivide()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, layerTriangle))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);
            triangle = hit.transform.gameObject;

                if (num != 8)
                {
                    hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, hit.transform.gameObject.name, true, inputLine.material.color,gameObject);

                    laserReset("Divide");
                }      
        }

    }

    void  LaserWall()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, layerWalls))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }
    void LaserStart()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, LayerStart))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }
    void LaserFinal()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, Mathf.Infinity, LayerFinal))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);


            laserFinal = hit.transform.gameObject;
            Vector3 direccion = hit.point - transform.position;
            hit.transform.gameObject.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material.color, direccion);
            laserReset("Final");

        }

    }

    bool LaserConfirm()
    {
        num = 0;
        if (Physics.Raycast(point, transformStart - point, out hit, Mathf.Infinity))
        {

            if (hit.transform.gameObject.layer == 10 || hit.transform.gameObject.layer == 7 || hit.transform.gameObject.layer == 8 || hit.transform.gameObject.layer == 11)
            {
                num = hit.transform.gameObject.layer;            
                return true;

            }
            return false;
        }
        return false;

    }

    bool ReflexConfirm()
    {
        numRef = 0;
        if (Physics.Raycast(point, transformStart - point, out hit, Mathf.Infinity))
        {

            if (hit.transform.gameObject.layer == 6)
            {
                numRef = hit.transform.gameObject.layer;
                return true;
            }
            return false;
        }
        return false;

    }

    void LaserDraw()
    {

        if ((LaserConfirm() && checkBool) || (ReflexConfirm() && checkBool1))
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
                case 11: //LaserStart
                    LaserFinal();
                    break;
            }
        }
        else
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }


    public void ReceiveImpactPoint(Vector3 _point,Vector3 _reflectiveRayPoint, bool _bool,Color _color, Vector3 _transformStart, GameObject _gameObject)
    {
        if(_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            reflectiveRayPoint = _reflectiveRayPoint;
            checkBool = _bool;
            TakeColor(_color);
            transformStart = _transformStart;

            LaserDraw();
        }
        
    }

    void TakeColor(Color _color)
    {
        for(int i = 0; i < mat.Length; i++)
        {
            if (mat[i].color == _color)
                inputLine.material = mat[i];
        }

    }


    public void ReflexiveMirror(Vector3 _point, Vector3 _reflectiveRayPoint, bool _bool, Color _color, Vector3 _transformStart, GameObject _gameObject)
    {
        if (_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            reflectiveRayPoint = _reflectiveRayPoint;
            checkBool1 = _bool;
            TakeColor(_color);
            transformStart = _transformStart;

            LaserDraw();
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
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Color":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero, null);
                reflexiveCube = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Divide":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero, null);
                reflexiveCube = null;
                objectRecvied = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Final":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position, null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color, null);
                triangle = null;
                break;

            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero, null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

        }
    }
}
