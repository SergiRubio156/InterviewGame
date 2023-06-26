using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cintamovement : MonoBehaviour
{
    public Rigidbody rb;
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * velocidad;
        }
    }
 
}
