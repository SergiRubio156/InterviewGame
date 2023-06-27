using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CerrarPantalla : MonoBehaviour
{
    public GameObject pantalla;
    public GameObject abrirbotton;
    public Button cerrarControl;
    public Button abrirControl;
    private bool pantallaVisible;

    // contador veces preguntar
    public int questionCont = 0;
    void Start()
    {
        if (GetCurrentSceneName() == "NIVEL 1")
        {
            pantalla.SetActive(true);
        }
        else
        {
            pantalla.SetActive(false);
        }
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

    public string GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }
    public void IncreaseQuestionCont()
    {
        questionCont++;
    }
}
