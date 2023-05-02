using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerColor : MonoBehaviour
{
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    Renderer rend;
    public float duration = 5f;
    private float timeElapsed = 0f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void objectInside(bool _bool)
    {
        meshRenderer.enabled = _bool;
        boxCollider.enabled = _bool;
    }

    public void ChangeColor(Renderer _rend,bool _bool)
    {
        Debug.Log("!");
        objectInside(_bool);

        timeElapsed += Time.deltaTime;

        // Calcula la cantidad de progreso según el tiempo transcurrido
        float progress = timeElapsed / duration;

        // Usa la función Lerp para interpolar entre el color actual y el color rojo
        Color newColor = Color.Lerp(_rend.material.color, Color.black, progress);

        // Actualiza el color del objeto
        _rend.material.color = newColor;
    }
}
