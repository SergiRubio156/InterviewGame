using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotPanel : MonoBehaviour, IPointerClickHandler
{
    ObjectManager objectManager;
    public Transform positionCube;
    [SerializeField]
    public GameObject[] prefabs = new GameObject[3];
    public GameObject parentObject; // GameObject padre que contiene el componente Horizontal Layout Group

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        Image clickedButton = clickedObject.GetComponent<Image>();

        if (clickedButton != null)
        {
            if (clickedButton.color == Color.red)
            {
                GameObject newImage = Instantiate(prefabs[0], positionCube);

                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                GameManager.Instance.State = GameState.Playing;
            }
            else if (clickedButton.name == "Image1")
            {
                GameObject newImage = Instantiate(prefabs[1], positionCube);

                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.color == Color.yellow)
            {


                GameManager.Instance.State = GameState.Playing;
            }
            else if (clickedButton.color == Color.green)
            {


                GameManager.Instance.State = GameState.Playing;
            }
        }

    }
}
