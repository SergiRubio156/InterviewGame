using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Rigidbody rb;

    //Objeto cogido
    Renderer rend;
    public GameObject objectOutline;
    public bool oneTime = false;
    float positionIntial;
    float distanceMax;


    bool isPlaying;
    int layerSpawn;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
        ObjectManager controller = FindObjectOfType<ObjectManager>();
        //controls = new StarterAssetsInputs();
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        isPlaying = (state == GameState.Settings);
    }
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            layerSpawn = 1 << 14;
            CheckGround();
            if (objectSelect)
                distanceMax = Mathf.Infinity;
            else
                distanceMax = 5;
            if (Input.GetKeyDown(KeyCode.Q)) 
            {
                Debug.Log("!");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
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

            if (hit.collider.tag == "Interactable" && !oneTime && !objectSelect)
            {
                objectOutline = hit.transform.gameObject;
                objectOutline.GetComponent<OutLineObject>().Outline(true, hit.transform.gameObject.transform.position);
                oneTime = true;
            }
            else if(hit.collider.tag != "Interactable" && oneTime)
            {
                if (objectOutline != null)
                    objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                oneTime = false;
                objectOutline = null;
            }

            if (Input.GetMouseButtonDown(0))
                RayObject();

            if (objectSelect)
            {
                Vector3 newPosition2 = poisitionHand.transform.position - objectHand.transform.position;
                objectHand.transform.position += newPosition2;//  * Time.deltaTime;

                if (hit.collider.tag == "Spawner" && !oneTime)
                {
                    objectOutline = hit.transform.gameObject;
                    objectOutline.GetComponent<OutLineObject>().Outline(true, hit.transform.gameObject.transform.position);
                    oneTime = true;

                }
                else if (hit.collider.tag != "Spawner" && oneTime)
                {
                    objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                    oneTime = false;
                    objectOutline = null;
                }
            }
            else if(positionMachine != null)
            {
                if (objectOutline != null)
                    objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                objectOutline = null;

                Vector3 newPosition2 = positionMachine.transform.position - objectHand.transform.position;
                objectHand.transform.position += newPosition2;//  * Time.deltaTime;

                StartCoroutine(Wait());
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
    void RayObject()
    {
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!objectSelect)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                    objectHand = hit.collider.gameObject;
                    if (objectManager.GetObjectPositionInList(objectHand))                  
                        objectManager.objectList[objectHand] = ObjectState.Taked;
                    rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                    rend = hit.collider.gameObject.GetComponent<Renderer>();
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    positionIntial = objectHand.transform.position.y;
                    objectSelect = true;
                    oneTime = false;
                    objectOutline = null;
                    positionMachine = null;
                }
                else if (hit.collider.CompareTag("Door"))
                {
                    objectButtonDoor = hit.collider.gameObject;
                    objectButtonDoor.GetComponent<TransitionCamera>().transitionScene();
                }
            }
        }
        else if (objectSelect)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Spawner"))
                {
                    positionMachine = hit.transform.gameObject;
                    positionMachine.GetComponent<SpawnerColor>().ChangeColor(rend,false);
                    objectSelect = false;
                }
                else if (positionMachine == null)
                {
                    objectHand.transform.position = new Vector3(objectHand.transform.position.x, positionIntial, objectHand.transform.position.z);
                    objectHand.GetComponent<Rigidbody>().velocity = objectHand.transform.position * 0f;
                    rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                    objectSelect = false;
                    objectHand = null;
                    oneTime = false;
                    objectOutline = null;
                    positionMachine = null;
                }
            }
        }
    }
}
