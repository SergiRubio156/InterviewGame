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
        }
        else
            outlineRenderer.enabled = false;
    }

    private void Update()
    {
        outlineRenderer.transform.position = transform.position;

    }
    Renderer CreateOutline(Material _mat, float _scaleFactor, Color _color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, positionCube, Quaternion.Euler(-270f, transform.rotation.y, transform.rotation.z));

        Renderer rend = outlineObject.GetComponent<Renderer>();

        Material[] materials = rend.materials;


        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = _mat;
            materials[i].SetColor("_OutlineColor", _color);
            materials[i].SetFloat("_Scale", _scaleFactor);
        }

        rend.materials = materials;

        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutLineObject>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        outlineObject.tag = "Untagged";
        //outlineObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        rb = outlineObject.GetComponent<Rigidbody>();

        rb.useGravity = false;

        rend.enabled = true;

        rend.transform.SetParent(transform, true);

        return rend;
    }
}
