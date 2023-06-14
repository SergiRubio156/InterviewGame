using UnityEngine;

public class Win1 : MonoBehaviour
{
    public GameObject pestaña; // Referencia al GameObject de la pestaña a activar para "Door"
    public GameObject pestañaLevel2; // Referencia al GameObject de la pestaña a activar para "Level2"

    private bool pestañaActivada = false; // Variable para controlar si la pestaña está activada o no

    private CharacterController characterController; // Referencia al CharacterController adjunto al personaje

    private void Start()
    {
        pestaña.SetActive(false); // Desactiva la pestaña para "Door"
        pestañaLevel2.SetActive(false); // Desactiva la pestaña para "Level2"

        // Obtén la referencia al CharacterController adjunto al GameObject del personaje
        characterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Comprueba si el personaje colisiona con el objeto etiquetado como "Door"
        if (hit.collider.CompareTag("Door") && !pestañaActivada)
        {
            pestaña.SetActive(true); // Activa la pestaña para "Door"
            pestañaActivada = true; // Marca la pestaña como activada
        }

        // Comprueba si el personaje colisiona con el objeto etiquetado como "Level2"
        if (hit.collider.CompareTag("Level2"))
        {
            pestañaLevel2.SetActive(true); // Activa la pestaña para "Level2"
        }
    }

    private void Update()
    {
        // Comprueba si el personaje ya no está colisionando con el objeto "Door"
        if (pestañaActivada && !IsCollidingWithDoor())
        {
            pestaña.SetActive(false); // Desactiva la pestaña para "Door"
            pestañaActivada = false; // Marca la pestaña como desactivada
        }

        // Comprueba si el personaje ya no está colisionando con el objeto "Level2"
        if (!IsCollidingWithLevel2())
        {
            pestañaLevel2.SetActive(false); // Desactiva la pestaña para "Level2"
        }
    }

    private bool IsCollidingWithDoor()
    {
        // Comprueba si el personaje está colisionando con un objeto etiquetado como "Door"
        Collider[] colliders = Physics.OverlapSphere(transform.position, characterController.radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Door"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCollidingWithLevel2()
    {
        // Comprueba si el personaje está colisionando con un objeto etiquetado como "Level2"
        Collider[] colliders = Physics.OverlapSphere(transform.position, characterController.radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Level2"))
            {
                return true;
            }
        }

        return false;
    }
}
