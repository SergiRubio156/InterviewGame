using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmPanel : MonoBehaviour, IPointerClickHandler
{
    ObjectManager objectManager;
    Objects obj;
    public Transform positionCube;
    [SerializeField]
    public GameObject[] prefabs = new GameObject[3];
    public GameObject parentObject; // GameObject padre que contiene el componente Horizontal Layout Group


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
            if (clickedButton.name == "Image1")
            {
                obj = objectManager.FindStateOfObject(ObjectState.Cables);

                parentObject = obj.obj;

                GameObject newImage = Instantiate(prefabs[0], parentObject.transform);


                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);


                obj.canMove = true;

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.name == "Image2")
            {
                obj = objectManager.FindStateOfObject(ObjectState.Cables);

                parentObject = obj.obj;

                GameObject newImage = Instantiate(prefabs[1], parentObject.transform);


                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                newImage.transform.rotation = Quaternion.Euler(0f, 257f, 0f);

                obj.canMove = true;

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.name == "Image3")
            {
                GameObject newImage = Instantiate(prefabs[2], Vector3.zero, Quaternion.identity);

                obj = objectManager.FindStateOfObject(ObjectState.Arm);

                int existingImagesCount = obj.obj.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                obj = objectManager.FindStateOfObject(ObjectState.Arm);

                obj.canMove = true;
                GameManager.Instance.State = GameState.Playing;

            }
        }

    }
}
