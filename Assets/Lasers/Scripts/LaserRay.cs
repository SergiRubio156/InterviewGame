using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LaserRay : MonoBehaviour
{
    int layerWalls,layerMirror,layerCylinder,layerTriangle, LayerStart, LayerFinal;

    public LineRenderer inputLine;

    RaycastHit hit;
    Vector3 reflectiveRayPoint;

    //GameObjects
     public GameObject cubeColor = null;
     public GameObject reflexive = null;
     public GameObject triangle = null;
     public GameObject laserFinal = null;
    public GameObject LaserObject = null;

    Vector3 positionLaser;
    //Rotacion 
    public float rotationSpeed;
    private float anguloActual = 0f;
    public bool isRotation = true;

    //
    public RaycastLine raycastLine;

    void Start()
    {
        positionLaser = LaserObject.transform.position;
        inputLine = LaserObject.GetComponentInChildren<LineRenderer>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
        rotationSpeed = 25;
        
    }

    private void GameManager_OnGameStateChanged(GameState state)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        //isRotation = (state == GameState.Settings);
    }
    void Update()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;
        positionLaser = LaserObject.transform.position;
        LaserDraw();
        //ObjectRotate();
        if (Input.GetKeyDown(KeyCode.Space)) //Cuando le damos click al Escape entra a esta funcion
        {
            isRotation = !isRotation;// = true ? isRotation : !isRotation;
        }

    }

    /*void ObjectRotate()
    {
        if (isRotation)
        {
            float rotacion = rotationSpeed * Time.deltaTime;

            anguloActual += rotacion;

            if (anguloActual >= 45f)
            {
                rotationSpeed = -rotationSpeed;
                anguloActual = 45f;
            }

            if (anguloActual <= -45f)
            {
                rotationSpeed = -rotationSpeed;
                anguloActual = -45f;
            }

            // Rotamos el objeto en el eje Y
            transform.Rotate(0f, rotacion, 0f);
        }


    }*/


    void LaserMirror()
    {
        Ray ray = new Ray(positionLaser, transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMirror))
        {

            Vector3 _hitPoint = hit.point;
            reflectiveRayPoint = Vector3.Reflect(_hitPoint - transform.position, hit.normal);

            inputLine.SetPosition(0, positionLaser);
            inputLine.SetPosition(1, _hitPoint);

            if(reflexive != hit.transform.gameObject && reflexive != null)
            {
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position,null);
                reflexive = null;
            }
            reflexive = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_hitPoint, reflectiveRayPoint, true, inputLine.material.color, transform.position,gameObject);

            laserReset("Mirror");
        }
    }

    void LaserColor()
    {
        Ray ray = new Ray(positionLaser, transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerCylinder))
        {
            inputLine.SetPosition(0, positionLaser);
            inputLine.SetPosition(1, hit.point);

            string _name = hit.transform.gameObject.name;
            if (cubeColor != hit.transform.gameObject && cubeColor != null)
            {
                cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;
            }
            cubeColor = hit.transform.gameObject;
            cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, true, _name);

            laserReset("Color");
        }
    }

    void LaserDivide()
    {
        Tuple<GameObject, Vector3,string,Material> objects = raycastLine.GetGameObjectAndPosition(positionLaser, transform.forward, layerTriangle, inputLine.material);

        triangle = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        triangle.GetComponentInParent<TriangleScript>().CheckPlane(_position, _name, true, _mat, this.gameObject);

        laserReset("Divide");

        /*if (triangle != hit.transform.gameObject && triangle != null)
        {
            triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material,null);
            triangle = null;
        }

        obj.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, _name, true, inputLine.material, this.gameObject);
        laserReset("Divide");      */

    }

    void LaserStart()
    {
        Tuple<GameObject, Vector3, string, Material> objects = raycastLine.GetGameObjectAndPosition(positionLaser, transform.forward, LayerStart, inputLine.material);

        GameObject obj = objects.Item1;
        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        laserReset("all");

    }
    void LaserFinal()
    {
        Ray ray = new Ray(positionLaser, transform.forward);
        if (Physics.Raycast(ray, out hit, 100, LayerFinal))
        {
            inputLine.SetPosition(0, positionLaser);
            inputLine.SetPosition(1, hit.point);


            laserFinal = hit.transform.gameObject;
            Vector3 direccion = hit.point - transform.position;
            hit.transform.gameObject.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material.color, direccion);
            laserReset("Final");
        }

    }



    void LaserWall()
    {
        Tuple<GameObject, Vector3, string, Material> objects = raycastLine.GetGameObjectAndPosition(positionLaser ,transform.forward ,layerWalls, inputLine.material);

        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        laserReset("all");
    }

    void LaserDraw()
    {
        switch (raycastLine.SearchLaser(positionLaser, transform.forward))
            {
                case 6:
                    //LaserMirror();
                    break;
                case 7:
                    //LaserColor();
                    break;
                case 8:
                    LaserDivide();
                    break;
                case 9:
                    LaserWall();
                    break;
                case 10:
                    LaserStart();
                    break;
                case 11:
                    //LaserFinal();
                    break;
            }

    }


    
    void laserReset(string _name)
    {
        switch(_name)
        {
            case "Mirror":
                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Color":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color,transform.position, null);
                reflexive = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Divide":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material.color, Vector3.zero);
                laserFinal = null;
                break;

            case "Final":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;
                break;

            case "all":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
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
