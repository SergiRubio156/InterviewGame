using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public Camera mainCamera;
    public ObjectManager objectManager;

    public bool objectSelect;
    public GameObject objectHand;
    public GameObject objectButtonDoor;
    float velocity = 5f;

    public GameObject poisitionHand;
    public GameObject positionMachine;

    //Objeto cogido
    public Material outline;
    public GameObject objectOutline;
    public bool oneTime = false;
    float positionIntial;
    float distanceMax;


    bool isPlaying;
    //int layerSpawn;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
        ObjectManager controller = FindObjectOfType<ObjectManager>();
        //controls = new StarterAssetsInputs();
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        isPlaying = (state == GameState.Settings || state == GameState.Wire);
    }

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            //layerSpawn = 1 << 14;
            CheckGround();
            if (objectSelect)
                distanceMax = Mathf.Infinity;
            else
                distanceMax = 5;
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, distanceMax, LayerMask.NameToLayer("NoInteractable")))
        {
            Vector3 curScreenPoint = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z);
            transform.position = curScreenPoint;

            if ((hit.collider.tag == "Interactable" || hit.collider.tag == "Door") && !objectSelect)
            {
                outline = hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material;
                outline.SetFloat("_Outline_Thickness", 0.01f);
            }
            else if ((hit.collider.tag != "Interactable" || hit.collider.tag == "Door") && !objectSelect)
            {
                outline.SetFloat("_Outline_Thickness", 0f);
            }

            if (objectSelect)
            {
                Vector3 newPosition2 = Vector3.Lerp(objectHand.transform.position, poisitionHand.transform.position, Time.deltaTime * 50);
                objectHand.transform.position = newPosition2;//  * Time.deltaTime;

                if (hit.collider.tag == "Spawner")
                {
                    outline = hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material;
                    outline.SetColor("_Outline_Color", Color.green);

                }
                else if (hit.collider.tag != "Spawner")
                {
                    if(objectHand.tag != "Interactable")
                        outline.SetColor("_Outline_Color", Color.white);
                    positionMachine = null;
                }
            }
            else if (positionMachine != null)
            {

                Vector3 newPosition2 = positionMachine.transform.position - objectHand.transform.position;
                objectHand.transform.position += newPosition2;//  * Time.deltaTime;

                StartCoroutine(Wait());
            }

            if (Input.GetMouseButtonDown(0))
            {
                Tuple<GameObject, string> objects = HitGameObject();
                SwitchList(objects.Item1, objects.Item2);
            }
        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        objectSelect = false;
        positionMachine = null;
        objectHand = null;
    }

    Tuple<GameObject,string> HitGameObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        string _tag = "";
        GameObject _object = null;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            _tag = hit.collider.tag;
            _object = hit.collider.gameObject;

            return Tuple.Create(_object, _tag);
        }

        return Tuple.Create(_object, _tag);
    }

    void SwitchList(GameObject _object,string _tag)
    {
        switch(_tag)
        {
            case "Interactable":
                if (!objectSelect)
                {
                    int i = objectManager.GetObjectPositionInList(_object);
                    if (i != -1)
                    {
                        if (objectManager.GetObjectStateInList(_object) != ObjectState.Taked)
                        {
                            if (outline != null)
                                outline.SetFloat("_Outline_Thickness", 0f);
                            objectManager.ObjectGameState(i, ObjectState.Taked);
                            objectHand = _object;
                            objectSelect = true;
                        }
                    }
                }
                break;
            case "Spawner":
                if (objectSelect)
                {

                    if (_object.name == "Wire")
                    {
                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            if (!objectManager.GetObjectBoolInList(w, "Wire"))
                            {
                                positionMachine = _object;
                                objectSelect = false;
                                objectManager.ObjectGameState(w, ObjectState.Cables);
                            }
                        }
                    }
                    else if (_object.name == "Color")
                    {
                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            if (!objectManager.GetObjectBoolInList(w, "Color"))
                            {
                                positionMachine = _object;
                                objectSelect = false;
                                objectManager.ObjectGameState(w, ObjectState.Color);
                            }
                        }
                    }
                    else if (_object.name == "Toppings")
                    {
                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            if (!objectManager.GetObjectBoolInList(w, "Toppings"))
                            {
                                positionMachine = _object;
                                objectSelect = false;
                                objectManager.ObjectGameState(w, ObjectState.Toppings);
                            }
                        }
                    }
                }
                break;
               
            case "Door":
                _object.GetComponent<TransitionCamera>().transitionScene("Door");
                break;

            default:
                if(objectSelect)
                { 
                    int d = objectManager.GetObjectPositionInList(objectHand);
                    if (d != -1)
                    {
                        if (objectManager.GetObjectStateInList(objectHand) == ObjectState.Taked)
                        {
                            objectManager.ObjectGameState(d, ObjectState.NoTaked);
                            objectHand.transform.position = new Vector3(objectHand.transform.position.x, positionIntial, objectHand.transform.position.z);
                            objectSelect = false;
                            objectHand = null;
                        }
                    }
                }
                break;
        }
    }

    void RayObject()
    {
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!objectSelect)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!objectSelect)
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        if (outline != null)
                            outline.SetFloat("_Outline_Thickness", 0f);
                        objectHand = hit.collider.gameObject;
                        int i = objectManager.GetObjectPositionInList(objectHand);
                        if (i != -1)
                        {
                            objectManager.ObjectGameState(i, ObjectState.Taked);
                            positionIntial = objectHand.transform.position.y;
                            objectSelect = true;
                            oneTime = false;
                            objectOutline = null;
                            positionMachine = null;
                        }
                        else
                        {
                            objectHand = poisitionHand;
                        }
                    }
                    else if (hit.collider.CompareTag("Door"))
                    {
                        objectButtonDoor = hit.collider.gameObject;
                        objectButtonDoor.GetComponent<TransitionCamera>().transitionScene("Door");
                    }
                }
            }
        }
        else if (objectSelect)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Spawner"))
                {
                    int i = objectManager.GetObjectPositionInList(objectHand);
                    if (i != -1)
                    { 
                        positionMachine = hit.transform.gameObject;
                        if(positionMachine.name == "Wire")
                            objectManager.ObjectGameState(i, ObjectState.Cables);
                        else if(positionMachine.name == "Color")
                            objectManager.ObjectGameState(i, ObjectState.Color);
                        objectSelect = false;
                    }
                    objectHand = null;
                }
                else if (positionMachine == null)
                {
                    objectHand = hit.collider.gameObject;
                    int i = objectManager.GetObjectPositionInList(objectHand);
                    if (i != -1)
                    {
                        objectManager.ObjectGameState(i, ObjectState.NoTaked);
                        objectHand.transform.position = new Vector3(objectHand.transform.position.x, positionIntial, objectHand.transform.position.z);
                        objectSelect = false;
                        objectHand = null;
                        oneTime = false;
                        objectOutline = null;
                        positionMachine = null;
                    }
                    objectHand = null;
                }
            }
        }
    }
}
