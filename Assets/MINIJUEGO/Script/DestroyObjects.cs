using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 9 && collision.gameObject.layer != 8)
        DestroyObject(collision.gameObject);
    }
}
