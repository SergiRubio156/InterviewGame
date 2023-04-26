using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject settingsMenu;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged; //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
    }

    private void GameManager_OnGameStateChanged(GameState state)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        //panelMenu.SetActive(state == GameState.Menu);   //Si el GameState es Menu se activa este panel
        settingsMenu.SetActive(state == GameState.Settings);        //Si el GameState es Settings se activa este panelç
        if (state == GameState.Settings || state == GameState.Lasers || state == GameState.Menu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public void StartGame() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Lasers
    }

    public void StartSettings() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.UpdateGameState(GameState.Settings);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Lasers
    }
    public void StartMenu() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.UpdateGameState(GameState.Menu);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Lasers
    }
    public void ExitToGame() 
    {
        GameManager.Instance.UpdateGameState(GameState.Exit);
    }
    public void ExitToPlay()
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
}                                                                                                                                                                                                                                                                                    
