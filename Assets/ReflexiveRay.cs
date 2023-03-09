using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    bool checking = false;
    public GameObject check;
    // Start is called before the first frame update
    void Start()
    {
        inputLine.SetPosition(0, Vector3.zero);
        inputLine.SetPosition(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (checking)
            Checks();
    }

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint)
    {
        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, reflectiveRayPoint * 3);

        if (Physics.Raycast(point, transform.forward, out hit, 100,8))
        {
            Debug.Log(hit.transform.name);
        }
        //checking = false;

    }

    public void Checks()
    {
        Debug.Log("hola");
        check.GetComponent<CheckLaser>().CheckLasers();
    }
}
