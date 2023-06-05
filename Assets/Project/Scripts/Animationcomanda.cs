using UnityEngine;
using UnityEngine.UI;

public class Animationcomanda : MonoBehaviour
{
    public Animator animatorController;
    public string animationName = "NombreDeLaAnimacion";
    public string animationclose = "NombreDeLaAnimacion";
    //public Button button;

    private void Start()
    {
        // Asegúrate de asignar el componente Animator al objeto en el Inspector de Unity.
        if (animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }

        // Asigna el método PlayAnimation al evento OnClick del botón.
        //button.onClick.AddListener(PlayAnimation);
    }

    public void PlayAnimation()
    {
        // Activa el trigger correspondiente al nombre de la animación.
        animatorController.SetTrigger(animationName);
    }
    public void PlayAnimation2()
    {
        // Activa el trigger correspondiente al nombre de la animación.
        animatorController.SetTrigger(animationclose);
    }
}
