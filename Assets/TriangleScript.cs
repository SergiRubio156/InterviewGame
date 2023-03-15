using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    public GameObject[] Sides = new GameObject[3];
    public LineRenderer[] LineSide = new LineRenderer[3];

    Vector3 localForward0 = new Vector3(0, 0, -1000);
    Vector3 localForward1 = new Vector3(0, 0, 1000);
    Vector3 localForward2 = new Vector3(1000, 0, 0);
    // Start is called before the first frame update
    void Start()
    {

        Render();
    }

    // Update is called once per frame
    void Update()
    {
        localForward0 = Sides[0].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward1 = Sides[1].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward2 = Sides[2].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;

    }
    void Render()
    {
        localForward0 = Sides[0].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward1 = Sides[1].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        localForward2 = Sides[2].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;

        LineSide[0].SetPosition(0, Sides[0].transform.position);
        LineSide[0].SetPosition(1, localForward0);
        LineSide[0].enabled = false;

        LineSide[1].SetPosition(0, Sides[1].transform.position);
        LineSide[1].SetPosition(1, localForward1);
        LineSide[1].enabled = false;

        LineSide[2].SetPosition(0, Sides[2].transform.position);
        LineSide[2].SetPosition(1, localForward2);
        LineSide[2].enabled = false;

    }
    public void CheckPlane(string _name)
    {
        RaycastHit hit;
        switch (_name)
        {
            case "1":
                /*LineSide[0].enabled = false;
                LineSide[1].enabled = true;
                LineSide[2].enabled = true;*/
                Debug.DrawRay(Sides[1].transform.position, localForward1, Color.red);

                if (Physics.Raycast(Sides[2].transform.position, localForward1, out hit, 1000f))
                {

                    Debug.Log("Victory");

                }
                break;
            case "2":
                Debug.DrawRay(Sides[1].transform.position, localForward1, Color.red);
                /*LineSide[0].enabled = true;
                LineSide[1].enabled = false;
                LineSide[2].enabled = true;*/
                break;
            case "3":
                Debug.DrawRay(Sides[1].transform.position, localForward1, Color.red);
                /*LineSide[0].enabled = true;
                LineSide[1].enabled = true;
                LineSide[2].enabled = false;*/
                break;
        }
    }
}