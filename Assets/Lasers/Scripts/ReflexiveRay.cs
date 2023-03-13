using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    bool checking = false;



    // Start is called before the first frame update
    void Start()
    {
        ResetLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (!checking)
            ResetLaser();
    }

    public void ReceiveImpactPoint(Vector3 point,Vector3 reflectiveRayPoint)
    {
        inputLine.gameObject.SetActive(true);
        checking = true;
        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, reflectiveRayPoint * 3);

        if (Physics.Raycast(point, -transform.forward, out hit, 100,8))
        {
            Debug.DrawRay(point, transform.forward, Color.green);
            hit.transform.gameObject.GetComponent<CheckLaser>().CheckLasers();
        }

    }

    void ResetLaser()
    {
        inputLine.gameObject.SetActive(false);
    }
    public void Checks(bool _chek)
    {
        checking = _chek;
    }
}
