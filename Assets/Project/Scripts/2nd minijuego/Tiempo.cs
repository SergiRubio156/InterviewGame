using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public float tiempomax;
    public float minutos = 0;
    public float segundos = 60;
    public TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((int)segundos == 0)
        {
            tiempomax--;
            segundos = 59; 
        } 
        else
        {
            segundos -= Time.deltaTime;
        }
        texto.text = " Tiempo: " + tiempomax.ToString() + ":" + segundos.ToString("#.");
    }
}
