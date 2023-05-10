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

}
