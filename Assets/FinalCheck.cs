using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheck : MonoBehaviour
{
    public int numFinalLaser = 0;
    public GameObject[] FinalLaser = new GameObject[0];
    public bool[] CheckBool;
 
    // Start is called before the first frame update
    void Awake()
    {
        FinalLaser = GameObject.FindGameObjectsWithTag("LaserFinal");
        CheckBool = new bool[FinalLaser.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckList(bool _bool, string _name)
    {
        for(int i = 0; i < FinalLaser.Length; i++)
        {
            if(FinalLaser[i].name == _name)
            {
                CheckBool[i] = _bool;
                if (CheckBools())
                    Debug.Log("Victoria");
            }

        }
    }

    bool CheckBools()
    {
        for (int b = 0; b < CheckBool.Length; b++)
        {
            if (!CheckBool[b])
                return false;
        }
        return true;
    }
}
