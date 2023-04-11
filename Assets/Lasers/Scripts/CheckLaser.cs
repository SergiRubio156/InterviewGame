using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaser : MonoBehaviour
{
    public GameObject finalCheck;
    public Material mat;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        finalCheck = this.gameObject.transform.parent.gameObject;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceivedLaser(bool _bool,Color _color)//,Vector3 _dir)
    {
        if (_bool)
        {
            animator.SetBool("recibe", _bool);
            if(ConfirmColor(_color))
            finalCheck.GetComponent<FinalCheck>().CheckList(_bool, gameObject.name);
        }
        else
        animator.SetBool("recibe", _bool);
    }

    bool ConfirmColor(Color _color)
    {
        if (mat.color == _color)
        {
            return true;
        }
        return false;
    }

}
