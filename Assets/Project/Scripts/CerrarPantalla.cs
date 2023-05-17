using UnityEngine;
using UnityEngine.UI;

public class CerrarPantalla : MonoBehaviour
{
    public GameObject pantalla;
    public GameObject abrirbotton;
    public Button cerrarControl;
    public Button abrirControl;
    private bool pantallaVisible;

    void Start()
    {
        pantalla.SetActive(false);
        abrirbotton.SetActive(true);
        pantallaVisible = false;
        cerrarControl.onClick.AddListener(CruzPantalla); 
        abrirControl.onClick.AddListener(interrogantePantalla); 

    }

    void interrogantePantalla()
    {
        pantallaVisible = !pantallaVisible;
        pantalla.SetActive (true);
        abrirbotton.SetActive (false);
    }    void CruzPantalla()
    {
        pantallaVisible = !pantallaVisible;
        abrirbotton.SetActive(true);
        pantalla.SetActive (false);
    

    }
}
