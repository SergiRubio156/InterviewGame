using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineObject : MonoBehaviour
{
    [SerializeField] private Material outiLineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        outlineRenderer = CreateOutline(outiLineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = false;
    }

    public void Outline(bool _bool)
    {
        if (_bool)
        {
            outlineRenderer.enabled = true;
        }
        else
            outlineRenderer.enabled = false;
    }

    Renderer CreateOutline(Material _mat, float _scaleFactor, Color _color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = _mat;
        rend.material.SetColor("_OutlineColor", _color);
        rend.material.SetFloat("_Scale", _scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutLineObject>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = true;

        rend.transform.SetParent(transform, true);

        return rend;
    }
}
