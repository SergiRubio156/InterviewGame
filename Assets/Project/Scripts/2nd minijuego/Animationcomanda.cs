using UnityEngine;
using UnityEngine.UI;

public class Animationcomanda : MonoBehaviour
{
    public Animator animatorController;
    public string animationOpen = "Abrir";
    public string animationClose = "Cerrar";
    public Button openButton;
    public Button closeButton;
    public GameObject paneltuto; 

    private void Start()
    {
        openButton.gameObject.SetActive(false);
        paneltuto.gameObject.SetActive(true);
        // Asegúrate de asignar el componente Animator al objeto en el Inspector de Unity.
        if (animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }

        // Asigna el método PlayAnimation a los eventos OnClick de los botones.
        openButton.onClick.AddListener(PlayOpenAnimation);
        closeButton.onClick.AddListener(PlayCloseAnimation);
    }

    public void PlayOpenAnimation()
    {
        // Activa el trigger correspondiente a la animación de abrir.
        animatorController.SetTrigger(animationOpen);

        // Desactiva el botón de abrir.
        openButton.gameObject.SetActive(false);

        // Activa el botón de cerrar.
        closeButton.gameObject.SetActive(true);
    }

    public void PlayCloseAnimation()
    {
        // Activa el trigger correspondiente a la animación de cerrar.
        animatorController.SetTrigger(animationClose);

        // Desactiva el botón de cerrar.
        closeButton.gameObject.SetActive(false);

        // Activa el botón de abrir.
        openButton.gameObject.SetActive(true);
    }
}
