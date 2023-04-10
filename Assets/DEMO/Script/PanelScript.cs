using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    GameObject islandManager;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        islandManager = GameObject.FindGameObjectWithTag("IslandManager");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if(rectTransform.anchoredPosition.y >= 80.0f)
        {
            islandManager.GetComponent<IslandManager>().RecievedCard();
            gameObject.SetActive(false);
        }
    }
}
