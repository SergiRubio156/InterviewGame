using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f;
    float rotationSpeed = 3f;
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 MovementDirection = new Vector3(horizontalInput, verticalInput, 0);

        transform.position += MovementDirection * speed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirection), rotationSpeed * Time.deltaTime);


    }

}
