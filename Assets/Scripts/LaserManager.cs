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
        GameState State;
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

        private void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
        {
            Cursor.visible = (state == GameState.Lasers && state == GameState.Settings);
            State = state;
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

        private void RaycastDetectObject()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxRayDistance))
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        ObjectMove = hit.collider.gameObject;

                    }
                }
            }


        }


    }
}
