using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsScript : MonoBehaviour
{
    public List<GameObject> Inventory;

    public List<GameObject> objects;

    public List<GameObject> cardsObjects;

    GameObject nuevaCarta;


    int i = 0;

    float desplazamientoHorizontal = 150.0f;

    public GameObject panel;
    public void AddInventory(string _layer)
    {
        foreach (GameObject obj in objects)
        {
            if (obj.layer == LayerMask.NameToLayer(_layer))
                Inventory.Add(obj);
        }
        CreateCard(_layer);
    }

    void CreateCard(string _layer)
    {
        foreach (GameObject obj in cardsObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(_layer))
            {
                nuevaCarta = Instantiate(cardsObjects[0], panel.transform.position, Quaternion.identity);
                nuevaCarta.transform.SetParent(panel.transform,false); ;
            }
            i++;
        }
    }

    public void DeleteObject(GameObject _object)
    {
        Debug.Log("!");
        Inventory.Remove(_object);
    }
}
