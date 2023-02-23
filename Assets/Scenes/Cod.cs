using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Cod : MonoBehaviour
{
    //Esto sirve para hacer apuntes
    /* esto tambien */
    int integer;
    float floating;
    char letra;
    string palabra;
    int[] arrayInts = new int[3];
    float[] arrayFloats = new float[10];
    // Start is called before the first frame update
    void Start()
    {
        integer = 0;
        floating = 3.2f;
        //ahora trabjamos con letras
        letra = 'b';
        palabra = "Barcelona";
        letra = palabra[3];

        //ahoratrabajamos con arrays (números enteros)
        /* arrayInts[0] = 1;
         arrayInts[1] = 123;
         arrayInts[2] = 256;
         */
        void Update()
        { }


        void DoFloats()
        {

            arrayFloats[0] = 1.0f;
            arrayFloats[1] = 2.0f;
            arrayFloats[2] = 3.0f;
            arrayFloats[3] = 4.0f;
            arrayFloats[4] = 5.0f;
            arrayFloats[5] = 6.0f;
            arrayFloats[6] = 7.0f;
            arrayFloats[7] = 8.0f;
            arrayFloats[8] = 9.0f;
            arrayFloats[9] = 10.0f;




            /* Debug.Log("El valor de la variable es: " + arrayFloats[0]);
             Debug.Log("El valor de la variable es: " + arrayFloats[1]);
             Debug.Log("El valor de la variable es: " + arrayFloats[2]);
             Debug.Log("El valor de la variable es: " + arrayFloats[3]);
             Debug.Log("El valor de la variable es: " + arrayFloats[4]);
             Debug.Log("El valor de la variable es: " + arrayFloats[5]);
             Debug.Log("El valor de la variable es: " + arrayFloats[6]);
             Debug.Log("El valor de la variable es: " + arrayFloats[7]);
             Debug.Log("El valor de la variable es: " + arrayFloats[8]);
             Debug.Log("El valor de la variable es: " + arrayFloats[9]);*/

            int[] cargador = new int[30];
            for (int i = 0; i < arrayFloats.Length/*en este caso10*/; ++i)
            {
                Debug.Log("El valor de la variable es: " + arrayFloats[i]);

            }
            for (int i = 0; i < arrayFloats.Length/*en este caso10*/; ++i)
            {
                int contador = i;
                Debug.Log("El valor de la variable es: - " + contador);
            }



        }
    }

    // Update is called once per frame
 
  
}
