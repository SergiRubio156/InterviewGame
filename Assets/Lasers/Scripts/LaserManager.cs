using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserManager : MonoBehaviour
{
    //private StarterAssetsInputs controls;
    public Camera mainCamera;
    public GameObject ObjectMove;
    public Transform objectSun;

    //Raycast
    float maxRayDistance = 100.0f;

    //Mouse
    bool sceneSettings;
    Vector3 worldPosition;
    bool objectSelect;
    public float velocity = 5f;
    int LayerPlane;

    //Rotacion 
    public float rotationSpeed = 1f;
    Quaternion currentRotation;
    public float targetRotation = 45f;
        
    //Rigidbody
    Rigidbody rb = null;
    public bool isPlaying = true;
    float positionIntial = 0f;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
                //controls = new StarterAssetsInputs();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;   //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
    }
    private void Start()
    {
        objectSelect = false;
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        if (state == GameState.Settings || state == GameState.Lasers)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        sceneSettings = (state == GameState.Settings);
        isPlaying = (state != GameState.Lasers);
    }


    // Update is called once per frame
    void Update()
    {
        LayerPlane = 1 << 13;

        if (Input.GetKeyDown(KeyCode.Escape)) //Cuando le damos click al Escape entra a esta funcion
        {
            if (sceneSettings) GameManager.Instance.State = GameState.Lasers;
            else GameManager.Instance.State = GameState.Settings;
        }
        if (!isPlaying)
        {
            CheckGround();
        }
    }


    void CheckGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerPlane))
        {
            
            Vector3 curScreenPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            transform.position = curScreenPoint;

            if (Input.GetMouseButtonDown(0))
                RayObject();

            if (objectSelect)
            {
                if (ObjectMove != null)
                {
                    if (positionIntial >= ObjectMove.transform.position.y)
                    {
                        Vector3 newPosition2 = new Vector3(hit.point.x, positionIntial, hit.point.z) - ObjectMove.transform.position;
                        ObjectMove.GetComponent<Rigidbody>().velocity = newPosition2 * velocity;
                        currentRotation = ObjectMove.transform.rotation;
                    }
                }
                else if(objectSun != null)
                    currentRotation = objectSun.rotation;
                Rotation();
            }

        }
    }

    void Rotation()
    {
        Quaternion targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;

        if (Input.GetKey(KeyCode.E))
        {
            targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;
            Quaternion newRotation = Quaternion.Lerp(currentRotation, targetQuaternion, Time.deltaTime * rotationSpeed);
            if (ObjectMove != null)
                ObjectMove.transform.rotation = newRotation;
            else if (objectSun != null)
                objectSun.rotation = newRotation;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            targetQuaternion = Quaternion.Euler(0, -targetRotation, 0) * currentRotation;
            Quaternion newRotation = Quaternion.Lerp(currentRotation, targetQuaternion, Time.deltaTime * rotationSpeed);
            if (ObjectMove != null)
                ObjectMove.transform.rotation = newRotation;
            else if (objectSun != null)
                objectSun.rotation = newRotation;
        }
    }

    void RayObject()
    {
                
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxRayDistance) && !objectSelect)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Interactable"))
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    objectSun = hit.collider.transform.Find("LaserSun");
                }
                else
                {
                    ObjectMove = hit.collider.gameObject;
                    positionIntial = ObjectMove.transform.position.y;

                }
                rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                //rb.drag = 9.9f;
                objectSelect = true;
            }
        }
        else if(objectSelect)
        {
            if(ObjectMove != null)
            {
                ObjectMove.transform.position = new Vector3(ObjectMove.transform.position.x, positionIntial, ObjectMove.transform.position.z);
                ObjectMove.GetComponent<Rigidbody>().velocity = ObjectMove.transform.position * 0f;
                ObjectMove = null;
            }
            if (objectSun != null)
            {
                objectSun = null;
            }
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb = null;
            objectSelect = false;
        }
    }
    
}
