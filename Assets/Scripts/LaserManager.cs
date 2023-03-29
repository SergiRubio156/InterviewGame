using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserManager : MonoBehaviour
{
        //private StarterAssetsInputs controls;
        public Camera mainCamera;
        public GameObject ObjectMove;

        //Raycast
        float maxRayDistance = 100.0f;

        //Mouse
        bool sceneSettings;
        Vector3 worldPosition;
        bool objectSelect;
        public float velocity = 5f;
        //Rotacion 
        public float rotationSpeed = 1f;
        Quaternion currentRotation;
        public float targetRotation = 45f;

        //Rigidbody
        Rigidbody rb = null;
        public bool isPlaying;

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
                Cursor.visible = true;

            sceneSettings = (state == GameState.Settings);
            isPlaying = (state != GameState.Settings);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //Cuando le damos click al Escape entra a esta funcion
            {
                if (sceneSettings) GameManager.Instance.UpdateGameState(GameState.Lasers);
                else GameManager.Instance.UpdateGameState(GameState.Settings);
            }
            if (isPlaying)
            {
                MoveMouse();
                if (objectSelect)
                    CheckGround();
            }
        }



        void MoveMouse()
        {
            worldPosition = Input.mousePosition;
            worldPosition.z = 10.0f;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, worldPosition.z);
            curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);

            transform.position = curScreenPoint;
            if (Input.GetMouseButtonDown(0))
                RayObject();
        }
        void CheckGround()
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxRayDistance,LayerMask.NameToLayer("NoInteractable")))
            {
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z) - ObjectMove.transform.position;
                ObjectMove.GetComponent<Rigidbody>().velocity = newPosition * velocity;
                currentRotation = ObjectMove.transform.rotation;
                Rotation();
            }
        }

        void Rotation()
        {
            Quaternion targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;

                if (Input.GetKey(KeyCode.E))
                {
                    targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;
                    Quaternion newRotation = Quaternion.Lerp(currentRotation, targetQuaternion, Time.deltaTime * rotationSpeed);
                    ObjectMove.transform.rotation = newRotation;
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    targetQuaternion = Quaternion.Euler(0, -targetRotation, 0) * currentRotation;
                    Quaternion newRotation = Quaternion.Lerp(currentRotation, targetQuaternion, Time.deltaTime * rotationSpeed);
                    ObjectMove.transform.rotation = newRotation;
                }
        }

        void RayObject()
        {
                float positionIntial = 0f;
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxRayDistance) && !objectSelect)
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        ObjectMove = hit.collider.gameObject;
                        rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                        positionIntial = ObjectMove.transform.position.y;
                        objectSelect = true;
                    }
                }
                else if(objectSelect)
                {
                    ObjectMove.transform.position = new Vector3(ObjectMove.transform.position.x, positionIntial, ObjectMove.transform.position.z);
                    ObjectMove.GetComponent<Rigidbody>().velocity = ObjectMove.transform.position * 0f;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb = null;
                    //StartCoroutine(Wait());
                    objectSelect = false;
                }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(.2f);
            ObjectMove = null;
        }
    
}
