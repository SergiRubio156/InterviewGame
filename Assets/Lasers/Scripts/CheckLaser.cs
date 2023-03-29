using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaser : MonoBehaviour
{
    public GameObject finalCheck;
    // Start is called before the first frame update
    void Awake()
    {
        finalCheck = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReceivedLaser(bool _bool)
    {
        finalCheck.GetComponent<FinalCheck>().CheckList(_bool,gameObject.name);
    }

}
