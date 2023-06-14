using UnityEngine;
using UnityEngine.UI;

public class CambioDiapositivas : MonoBehaviour
{
    public GameObject[] diapositivas;
    private int diapositivaActual;
    private int totalDiapositivas;

    public GameObject tuto;
    public GameObject juego;

    void OnEnable()
    {
        // Mostrar la primera diapositiva cuando el objeto se activa
        MostrarDiapositiva(0);
    }

    void Start()
    {
        diapositivaActual = 0;
        totalDiapositivas = diapositivas.Length;

        // Ocultar todas las diapositivas excepto la primera
        for (int i = 1; i < totalDiapositivas; i++)
        {
            diapositivas[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CambiarDiapositiva();
        }
    }

    void CambiarDiapositiva()
    {
        // Ocultar la diapositiva actual
        diapositivas[diapositivaActual].SetActive(false);

        // Avanzar a la siguiente diapositiva o volver a la primera si alcanza el final
        diapositivaActual++;
        if (diapositivaActual >= totalDiapositivas)
        {
            diapositivaActual = 0;
        }

        // Mostrar la nueva diapositiva
        MostrarDiapositiva(diapositivaActual);

        // Verificar si es la quinta diapositiva y desactivar el panel "tuto" y activar el panel "juego"
        if (diapositivaActual == 4)
        {
            tuto.SetActive(false);
            juego.SetActive(true);
        }
    }

    void MostrarDiapositiva(int index)
    {
        diapositivas[index].SetActive(true);
    }

    // Activar el panel "TUTORIALrobot" y desactivar el panel "COMANDA PANEL SINO NO SE ACTIVARÁ"
}
