using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    GameObject player;
    GameObject islandManager;

    GameObject cardsObject;
    public float DurationOfTree = 5.0f;
    public string layerObject;
    // Start is called before the first frame update

    void Awake()
    {
        cardsObject = GameObject.FindGameObjectWithTag("Cards");
        islandManager = GameObject.FindGameObjectWithTag("IslandManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        if ("Player" == other.gameObject.name)
        {
            player = other.gameObject;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(DurationOfTree);
        cardsObject.GetComponent<CardsScript>().AddInventory(layerObject);
        player.GetComponent<PlayerMove>().FinishAction(false,gameObject);
        islandManager.GetComponent<IslandManager>().DeleteObject(gameObject);
        Destroy(gameObject);
    }

}
