using UnityEngine;
using UnityEngine.UI;

public class CambioDiapositivas : MonoBehaviour
{
    public GameObject[] diapositivas;
    private int diapositivaActual;
    private int totalDiapositivas;

    public Button nextButton; // Referencia al botón "NEXT DERECHO"
    public Button prevButton; // Referencia al botón "PREV IZQUIERDO"

    void Start()
    {
        diapositivaActual = 0;
        totalDiapositivas = diapositivas.Length;

        // Mostrar la primera diapositiva
        MostrarDiapositiva(diapositivaActual);

        // Ocultar las diapositivas restantes
        for (int i = 1; i < totalDiapositivas; i++)
        {
            diapositivas[i].SetActive(false);
        }

        // Asignar las funciones OnNextButtonClick() y OnPrevButtonClick() a los eventos de clic de los botones
        nextButton.onClick.AddListener(OnNextButtonClick);
        prevButton.onClick.AddListener(OnPrevButtonClick);
    }

    void OnNextButtonClick()
    {
        // Ocultar la diapositiva actual
        diapositivas[diapositivaActual].SetActive(false);

        // Avanzar a la siguiente diapositiva
        diapositivaActual++;
        if (diapositivaActual >= totalDiapositivas)
        {
            diapositivaActual = 0;
        }

        // Mostrar la nueva diapositiva
        MostrarDiapositiva(diapositivaActual);
    }

    void OnPrevButtonClick()
    {
        // Ocultar la diapositiva actual
        diapositivas[diapositivaActual].SetActive(false);

        // Retroceder a la diapositiva anterior
        diapositivaActual--;
        if (diapositivaActual < 0)
        {
            diapositivaActual = totalDiapositivas - 1;
        }

        // Mostrar la nueva diapositiva
        MostrarDiapositiva(diapositivaActual);
    }

    void MostrarDiapositiva(int index)
    {
        diapositivas[index].SetActive(true);
    }
}
