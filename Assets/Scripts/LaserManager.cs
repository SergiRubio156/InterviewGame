using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class LaserManager : MonoBehaviour
    {
        private StarterAssetsInputs controls;
        public Camera mainCamera;
        public GameObject ObjectMove;

        void Awake()
        {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
            mainCamera = Camera.main;
            controls = new StarterAssetsInputs();
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;   //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
        }

        private void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
        {
            Cursor.visible = (state == GameState.Lasers);
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //Cuando le damos click al Escape entra a esta funcion
            {
                GameManager.Instance.UpdateGameState(GameState.Settings);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Settings
            }

        }

        private void DetectObject()
        {
           //Ray ray = mainCamera.ScreenPointToRay(controls.mousePosition.
           controls.pla
        }


    }
}
