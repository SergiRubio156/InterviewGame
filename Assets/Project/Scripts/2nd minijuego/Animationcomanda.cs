using UnityEngine;
using UnityEngine.UI;

public class Animationcomanda : MonoBehaviour
{
    public Animator animatorController;
    public string animationOpen = "Abrir";
    public string animationClose = "Cerrar";
    public GameObject panelTuto;

    private void Start()
    {
        panelTuto.SetActive(true);
        // Asegúrate de asignar el componente Animator al objeto en el Inspector de Unity.
        if (animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayCloseAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlayOpenAnimation();
        }
    }

    public void PlayOpenAnimation()
    {
        // Activa el trigger correspondiente a la animación de abrir.
        animatorController.SetTrigger(animationOpen);
        panelTuto.SetActive(false);
    }

    public void PlayCloseAnimation()
    {
        // Activa el trigger correspondiente a la animación de cerrar.
        animatorController.SetTrigger(animationClose);
        panelTuto.SetActive(true);
    }
}
