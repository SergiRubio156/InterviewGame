using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineObject : MonoBehaviour
{
    [SerializeField] private Material outiLineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    [SerializeField] private Rigidbody rb;
    private Renderer outlineRenderer;

    Vector3 positionCube;
    bool oneTime;
    // Start is called before the first frame update
    void Start()
    {
        positionCube = transform.position;
        outlineRenderer = CreateOutline(outiLineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = false;
    }

    public void Outline(bool _bool,Vector3 _position)
    {
        if (_bool)
        {
            outlineRenderer.enabled = true;
            outlineRenderer.transform.position = _position;
        }
        else
            outlineRenderer.enabled = false;
    }

    Renderer CreateOutline(Material _mat, float _scaleFactor, Color _color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, positionCube, transform.rotation);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = _mat;
        rend.material.SetColor("_OutlineColor", _color);
        rend.material.SetFloat("_Scale", _scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutLineObject>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        rb = outlineObject.GetComponent<Rigidbody>();

        rb.useGravity = false;

        rend.enabled = true;

        rend.transform.SetParent(transform, true);

        return rend;
    }
}
