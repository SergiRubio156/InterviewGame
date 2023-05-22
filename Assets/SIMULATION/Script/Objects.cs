using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[System.Serializable]
public class Objects : ObjectManager
{

    public int id;
    public GameObject name;
    public ObjectState state;
    
    public BoxCollider boxCollider;
    public Rigidbody rb;

    public bool cablesCheck = false;
    public bool toppingCheck = false;
    public bool canMove = false;

    //COLOR

    public bool colorCheck = false;
    float durationColor = 20f;
    public Gradient gradient;
    float time = 0f;
    public Renderer rend;
    public Color currentColor;
    public Material outline;

    private void Start()
    {
        name = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        rend = GetComponentInChildren<Renderer>();
        outline = GetComponentInChildren<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
    }

    public override void ObjectNoTaked()
    {
        boxCollider.enabled = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        outline.SetFloat("_Outline_Thickness", 0.01f);
    }

    public override void ObjectTaked()
    {
        boxCollider.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public override void ObjectCables()
    {
        if (!cablesCheck)
        {
            StartCoroutine(Wait());
            cablesCheck = true;
        }
        cablesCheck = true;
    }

    public override void ObjectColors()
    {
        boxCollider.enabled = true;
        colorCheck = false;
        StartCoroutine(WaitColor());
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => canMove);
        boxCollider.enabled = true;
        GameManager.Instance.State = GameState.Wire;

    }
    private IEnumerator WaitColor()
    {
        yield return StartCoroutine(LerpPosition());

    }

    private IEnumerator LerpPosition()
    {
  
        while (!colorCheck && (time < durationColor))
        {
            Debug.Log(state);
            currentColor = gradient.Evaluate(time / durationColor);

            time += Time.deltaTime;

            // Combinar el color del gradiente y el color de la textura

            rend.materials[1].color = currentColor;

            if(state == ObjectState.Taked)
            {
                Debug.Log("!");
                colorCheck = true;
            }
            yield return null;
        }
    }

}
