using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    private float puntos;

    public TextMeshProUGUI textMesh;


    void Start()
    {
    }

    private void Update()
    {
        textMesh.text = puntos.ToString("0");
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GoodObjects")
        {
            ++puntos; 
        }
        else if (collision.gameObject.tag == "BadObjects")
        {
            --puntos;
        }
    }
}
