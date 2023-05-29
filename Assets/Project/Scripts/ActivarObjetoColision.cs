using UnityEngine;
using UnityEngine.UI;

public class ActivarObjetoColision : MonoBehaviour
{
    public GameObject objetoActivar;
    public GameObject referencia;
    public string jugadorTag;

    private Vector3 posicionInicial;

    public Button botonTeletransporte;

    private bool jugadorTeletransportado;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(jugadorTag))
        {
            objetoActivar.SetActive(true);
        }
    }

    private void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(jugadorTag);
        if (jugador != null)
        {
            posicionInicial = jugador.transform.position;
            jugadorTeletransportado = false;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta de jugador: " + jugadorTag);
        }

        if (botonTeletransporte != null)
        {
            botonTeletransporte.onClick.AddListener(ResetearJugador);
        }
        else
        {
            Debug.LogError("No se asignó un botón de teletransporte en el script ActivarObjetoColision.");
        }
    }

    public void MoverAJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(jugadorTag);
        if (jugador != null)
        {
            jugador.transform.position = referencia.transform.position;
            jugadorTeletransportado = true;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta de jugador: " + jugadorTag);
        }
    }

    public void ResetearJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(jugadorTag);
        if (jugador != null)
        {
            jugador.transform.position = posicionInicial;
            jugadorTeletransportado = false;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta de jugador: " + jugadorTag);
        }
    }
}
