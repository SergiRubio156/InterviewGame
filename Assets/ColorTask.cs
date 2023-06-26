using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorTask : MonoBehaviour, IPointerClickHandler
{
    ObjectManager objectManager;
    [SerializeField]
    public Gradient[] gradientPresets = new Gradient[4];

    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        Image clickedButton = clickedObject.GetComponent<Image>();

        if (clickedButton != null)
        {
            if (clickedButton.color == Color.red)
            {
                Objects obj = objectManager.FindStateOfObject(ObjectState.Color);

                obj.gradient = gradientPresets[0];

                obj.colorCheck = true;

                GameManager.Instance.State = GameState.Playing;
            }
            else if (clickedButton.color == Color.blue)
            {
                Objects obj = objectManager.FindStateOfObject(ObjectState.Color);

                obj.gradient = gradientPresets[1];

                obj.colorCheck = true;

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.color == Color.yellow)
            {
                Objects obj = objectManager.FindStateOfObject(ObjectState.Color);

                obj.gradient = gradientPresets[2];

                obj.colorCheck = true;

                GameManager.Instance.State = GameState.Playing;
            }
            else if (clickedButton.color == Color.green)
            {
                Objects obj = objectManager.FindStateOfObject(ObjectState.Color);

                obj.gradient = gradientPresets[3];

                obj.colorCheck = true;

                GameManager.Instance.State = GameState.Playing;
            }
        }

    }
}








