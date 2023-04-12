using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaser : MonoBehaviour
{
    public GameObject finalCheck;
    public Material mat;
    Animator animator;
    public Transform armatureDir;

    float velocidadRotacion = 5.0f;
    // Start is called before the first frame update
    void Awake()
    {
        finalCheck = this.gameObject.transform.parent.gameObject;
        animator = gameObject.GetComponent<Animator>();
        armatureDir = transform.Find("Armature.001/Bone/Bone.001");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceivedLaser(bool _bool,Color _color,Vector3 _dir)
    {
        if (_bool)
        {
            float anguloZ = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotacion = Quaternion.Euler(0f, 0f, anguloZ);
            Debug.Log(anguloZ);
            armatureDir.rotation = Quaternion.Lerp(armatureDir.rotation, rotacion, velocidadRotacion * Time.deltaTime);


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
