using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    public List<Vector3> vectorRecibed = new List<Vector3>()
    {
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    };

    GameObject[] reflexive = new GameObject[3];
     GameObject[] cubeColor = new GameObject[3];
     GameObject[] triangle = new GameObject[3];
     GameObject[] laserFinal = new GameObject[3];
     public Material[] mat = new Material[4];

    LineRenderer[] inputLine = new LineRenderer[2];
    public GameObject[] inputLineObject = new GameObject[2];

    public GameObject LaserObject = null;

    Vector3 positionLaser;
    public GameObject objectRecvied;

    RaycastHit hit;


    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    public Vector3 point;

    public bool checkBool;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = LaserObject.GetComponent<Renderer>();
        positionLaser = LaserObject.transform.position;
        for (int i = 0; i < vectorRecibed.Count; i++)
        {
            reflexive[i] = null;
            cubeColor[i] = null;
            triangle[i] = null;
            laserFinal[i] = null;
            inputLine[i] = inputLineObject[i].GetComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        //layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;

         layerTriangle = ~1 << 8;

        Render();
    }

    public bool ConfirmLine(int _name)
    {
        return true;
    }
    void Render()
    {
    }


    void LaserMirror(int i)
    {

    }

    void LaserColor(int i)
    {

    }

    void LaserDivide(int i)
    {


    }

    void LaserStart(int i)
    {


    }

    void LaserWall(int i)
    {


    }
    void LaserFinal(int i)
    {


    }
    void LaserDraw()
    {
        if (checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                SearchLaser(i);
                /*switch (SearchLaser(i))
                {
                    case 6:
                        LaserMirror(i);
                        break;
                    case 7:
                        LaserColor(i);
                        break;
                    case 8:
                        LaserDivide(i);
                        break;
                    case 9:
                        LaserWall(i);
                        break;
                    case 10:
                        LaserStart(i);
                        break;
                    case 11:
                        LaserFinal(i);
                        break;
                    default:
                        LaserWall(i);
                        break;
                }*/
            }
        }
        /*else if (!checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                inputLine[i].SetPosition(0, Vector3.zero);
                inputLine[i].SetPosition(1, Vector3.zero);
            }

        }*/

    }

    void SearchLaser(int i)
    {
        Ray ray = new Ray(transform.position, vectorRecibed[i]);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("Triangle")))
        {
            Debug.Log("1");
            inputLine[i].SetPosition(0, point);
            inputLine[i].SetPosition(1, hit.point);

            //return hit.transform.gameObject.layer;
        }
        //return 0;
    }

    public void CheckPlane(Vector3 _point,Vector3 _dir, bool _bool,Material _mat, GameObject _gameObject)
    {
        if (_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;

            Vector3 vectorRecibeds = _dir;

            float angulo = 45f;
            vectorRecibed[0] = Quaternion.AngleAxis(angulo, Vector3.up) * vectorRecibeds; // Vector rotado 45 grados alrededor del eje Y
            vectorRecibed[1] = Quaternion.AngleAxis(-angulo, Vector3.up) * vectorRecibeds; // Vector rotado 45 grados alrededor del eje Y


            inputLine[0].material = _mat;
            inputLine[1].material = _mat;
            renderer.material = _mat;

            


            if (_point == Vector3.zero)
            {
                checkBool = false;
            }

            checkBool = _bool;
            //TakeColor(_mat);
            LaserDraw();
        }
    }

    void TakeColor(Material _mat)
    {
        for (int i = 1; i <= inputLine.Length; i++)
        {
            //if (mat[i] == _mat)
            //{
                inputLine[i].material = _mat;
                inputLine[i].material = _mat;

            //}
        }

    }
    void laserReset(string _name, int i)
    {

    }
}