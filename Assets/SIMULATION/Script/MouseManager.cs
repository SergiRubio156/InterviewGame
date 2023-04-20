using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Camera mainCamera;

    bool objectSelect;
    public GameObject objectMove;
    public GameObject objectSelected;
    public float velocity = 5f;

    public GameObject objectHand;
    Rigidbody rb;
    GameObject objectOutline;
    bool oneTime = false;
    float positionIntial;
    float distanceMax;
    bool isPlaying;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
        //controls = new StarterAssetsInputs();
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        isPlaying = (state == GameState.Settings);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
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

            if (hit.collider.tag == "Interactable" && !oneTime && !objectSelect)
            {
                objectOutline = hit.transform.gameObject;
                objectOutline.GetComponent<OutLineObject>().Outline(true, hit.transform.gameObject.transform.position);
                oneTime = true;
            }
            else if(hit.collider.tag != "Interactable" && oneTime)
            {
                objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                oneTime = false;
                objectOutline = null;
            }
                if (Input.GetMouseButtonDown(0))
                RayObject();

            if (objectSelect)
            {
                Vector3 newPosition2 = objectHand.transform.position - objectMove.transform.position;
                objectMove.transform.position += newPosition2;//  * Time.deltaTime;
            }
        }
    }

    void RayObject()
    {
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && !objectSelect)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                objectOutline.GetComponent<OutLineObject>().Outline(false, hit.transform.gameObject.transform.position);
                objectMove = hit.collider.gameObject;
                rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                positionIntial = objectMove.transform.position.y;
                objectSelect = true;
            }
        }
        else if (objectSelect)
        {
            objectMove.transform.position = new Vector3(objectMove.transform.position.x, positionIntial, objectMove.transform.position.z);
            objectMove.GetComponent<Rigidbody>().velocity = objectMove.transform.position * 0f;
            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            objectSelect = false;
            oneTime = false;
            objectOutline = null;
        }
    }
}
