using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cintamovement : MonoBehaviour
{
    Rigidbody rb;
    public float velocidad;
    RandomImageGenerator randomImageGenerator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomImageGenerator = GameObject.Find("RawImageRandomGenerator").GetComponent<RandomImageGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("cinta"))
        {
            rb.velocity = transform.forward * velocidad;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Plane"))
            {
                randomImageGenerator.GenerateNewRobot();
                randomImageGenerator.GenerateNewRobot();
                Destroy(this.gameObject);
            }
        }
    }
}
