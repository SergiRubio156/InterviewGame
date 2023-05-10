using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{

    public Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetColor(Color _color)
    {
        _image.color = _color;
    }
}
