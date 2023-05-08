using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objects : ObjectManager
{

    public int id;
    public GameObject name;
    public ObjectState state;
    //public BoxCollider boxCollider;
    //public Rigidbody rb;

    public bool CablesCheck = false;
    private void Start()
    {
        name = this.gameObject;
        //boxCollider = GetComponent<BoxCollider>();
        //rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    /*public virtual void ObjectNoTaked()
    {
        //StartCoroutine((wait()));
        //boxCollider.enabled = true;
        //this.rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    public virtual void ObjectTaked()
    {
        //StartCoroutine((wait()));
        //boxCollider.enabled = false;
        //this.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public virtual void ObjectCables()
    {
        if (!CablesCheck)
        {
            GameManager.Instance.UpdateGameState(GameState.Wire);
            CablesCheck = true;
        }
    }*/

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
    }
    
}
