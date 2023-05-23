using UnityEngine;
using UnityEngine.UI;

public class ActivarObjetoColision : MonoBehaviour
{
    public GameObject objetoActivar;

    public GameObject referencia;
    public GameObject jugador;

    private Vector3 posicionInicial;

    public Button botonTeletransporte;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            objetoActivar.SetActive(true);
        }
    }

    private void Start()
    {
        posicionInicial = jugador.transform.position;
        botonTeletransporte.onClick.AddListener(MoverAJugador);
    }

    public void MoverAJugador()
    {
        jugador.transform.position = referencia.transform.position;
    }

    public void ResetearJugador()
    {
        jugador.transform.position = posicionInicial;
    }
}
