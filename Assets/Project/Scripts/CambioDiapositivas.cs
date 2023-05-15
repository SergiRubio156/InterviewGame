using UnityEngine;
using UnityEngine.UI;

public class CambioDiapositivas : MonoBehaviour
{
    public GameObject[] diapositivas;
    private int diapositivaActual;
    private int totalDiapositivas;

    public Button nextButton; // Referencia al botón "NEXT DERECHO"

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

        // Asignar la función OnNextButtonClick() al evento de clic del botón "NEXT DERECHO"
        nextButton.onClick.AddListener(OnNextButtonClick);
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

    void MostrarDiapositiva(int index)
    {
        diapositivas[index].SetActive(true);
    }
}
