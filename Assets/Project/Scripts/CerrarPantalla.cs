using UnityEngine;
using UnityEngine.UI;

public class CerrarPantalla : MonoBehaviour
{
    public GameObject pantalla;
    public Button botonabrir;
    public Button botonControl;
    private bool pantallaVisible;

    void Start()
    {
        pantallaVisible = false;
        botonControl.onClick.AddListener(ControlarPantalla); 
        botonabrir.onClick.AddListener(AbrirPantalla);

    }

    void ControlarPantalla()
    {
        pantallaVisible = !pantallaVisible;
        pantalla.SetActive(pantallaVisible);
    }
    void AbrirPantalla()
    {
        pantallaVisible = !pantallaVisible;
        pantalla.SetActive(pantallaVisible);
    }
}
