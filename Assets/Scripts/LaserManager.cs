using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class LaserManager : MonoBehaviour
    {
        //private StarterAssetsInputs controls;
        public Camera mainCamera;
        public GameObject ObjectMove;
        public GameObject Mirror;


        float maxRayDistance = 100.0f;

        bool sceneSettings;
        Vector3 worldPosition;


        private Vector3 screenPoint;
        private Vector3 offset;
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
            Cursor.visible = true;
        }

        private void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
        {
            Cursor.visible = (state == GameState.Lasers && state == GameState.Settings);
            sceneSettings = (state == GameState.Settings);
        }


        // Update is called once per frame
        void Update()
        {

            RaycastDetectObject();
            if (Input.GetKeyDown(KeyCode.Escape)) //Cuando le damos click al Escape entra a esta funcion
            {
                if (sceneSettings) GameManager.Instance.UpdateGameState(GameState.Lasers);
                else GameManager.Instance.UpdateGameState(GameState.Settings);
            }
        }

        void RaycastDetectObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxRayDistance))
                {
                    Debug.DrawRay(worldPosition, Vector3.forward, Color.red, maxRayDistance);
                    if(hit.collider.CompareTag("Interactable"))
                    {
                        ObjectMove = hit.collider.gameObject;
                        //MoveObject();
                    }
                }
            }
        }

        void MoveObject()
        {
            Debug.Log("Entra");
            worldPosition = Input.mousePosition;
            worldPosition.z = 10.0f;

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, worldPosition.z);
            curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            if (curScreenPoint.y <= 0)
                curScreenPoint.y = 0.02f;
            ObjectMove.transform.position = curScreenPoint;
        }


        private void OnMouseDrag()
        {
            Debug.Log("Entra");
            worldPosition = Input.mousePosition;
            worldPosition.z = 10.0f;

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, worldPosition.z);
            curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            if (curScreenPoint.y <= 0)
                curScreenPoint.y = 0.02f;
            ObjectMove.transform.position = curScreenPoint;
        }

    }
}
