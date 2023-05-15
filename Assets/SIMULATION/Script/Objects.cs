using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objects : ObjectManager
{

    public int id;
    public GameObject name;
    public ObjectState state;
    
    public BoxCollider boxCollider;
    public Rigidbody rb;

    public bool cablesCheck = false;
    public bool canMove = false;

    public GameObject arms;
    private void Start()
    {
        name = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        //rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    public override void ObjectNoTaked()
    {
        boxCollider.enabled = true;
        //this.rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

    public override void ObjectTaked()
    {
        boxCollider.enabled = false;
        //this.gameObject.layer = LayerMask.NameToLayer("NoInteractable");
        //this.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public override void ObjectCables()
    {
        if (!cablesCheck)
        {
            StartCoroutine(Wait());
            cablesCheck = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => canMove);//WaitForSeconds(0.5f);//WaitUntil(() => canMove);
        GameManager.Instance.UpdateGameState(GameState.Wire);

    }
}
