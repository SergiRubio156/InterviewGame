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

    GameObject card;
    CardsScript cardsScript;
    Image image;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        islandManager = GameObject.FindGameObjectWithTag("IslandManager");
        image = GetComponent<Image>();
        card = GameObject.FindGameObjectWithTag("Cards");
        cardsScript = card.GetComponent<CardsScript>();

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
            image.enabled = false;
            cardsScript.DeleteObject(gameObject);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
