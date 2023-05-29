using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject settingsMenu;
    public GameObject WireMenu;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        switch (newState)
        {
            case GameState.Playing:
                settingsMenu.SetActive(false);
                WireMenu = GameObject.FindGameObjectWithTag("WirePanel");
                if (WireMenu != null)
                    WireMenu.SetActive(false);
                break;
            case GameState.Lasers:
                settingsMenu.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Settings:
                settingsMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Menu:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Wire:
                WireMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }


    public void StartGame() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Playing;
    }

    public void StartSettings() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Settings;
    }
    public void StartMenu() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Menu;
    }
    public void ExitToGame() 
    {
        GameManager.Instance.State = GameState.Exit;
    }
    public void ExitToPlay()
    {
        GameManager.Instance.State = GameState.Playing;
    }
}                                                                                                                                                                                                                                                                                    
