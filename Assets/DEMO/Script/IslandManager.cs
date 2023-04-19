using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class IslandManager : MonoBehaviour
{
    public Camera mainCamera;
    public PlayerMove playerMove;
    public List<GameObject> allObjects = new List<GameObject>();
    public List<GameObject> objectFind = new List<GameObject>();
    GameObject[] objects;
    public int random;
    public Image imagenUI;


    public GameObject ObjectMove;
    Rigidbody rb = null;


    //Mouse
    bool sceneSettings;
    Vector3 worldPosition;
    public bool objectSelect;
    public float velocity = 5f;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
        FindObjects();
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    public void FindObjects()
    {
        objects = GameObject.FindGameObjectsWithTag("ObjectFind");
        foreach (GameObject item in objects)
        {
            if (!objectFind.Contains(item)) 
                objectFind.Add(item);
        }
        if (objectFind.Count != 0)
            playerMove.SendList(objectFind);
    }

    public void DeleteObject(GameObject _object)
    {
        objectFind.Remove(_object);
    }

    void InsertObject()
    {
        ObjectMove.transform.position = new Vector3(ObjectMove.transform.position.x, ObjectMove.transform.position.y, ObjectMove.transform.position.z);
        ObjectMove.GetComponent<Rigidbody>().velocity = ObjectMove.transform.position * 0f;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb = null;
        FindObjects();
        objectSelect = false;
    }

    public void RecievedCard()
    {
        if (!objectSelect)
        {
            ObjectMove = Instantiate(allObjects[0], transform.position, Quaternion.identity);
            rb = ObjectMove.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        }
        objectSelect = true;
    }

    void CheckGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("NoInteractable")))
        {
            Vector3 curScreenPoint = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z);
            transform.position = curScreenPoint;

            if (objectSelect)
            {
                Vector3 newPosition2 = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z) - ObjectMove.transform.position;
                ObjectMove.GetComponent<Rigidbody>().velocity = newPosition2 * velocity;
                if (Input.GetMouseButtonDown(0))
                    InsertObject();
            }

        }
    }
}
