using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject settingsMenuPlay;
    public GameObject settingsMenuLasers;
    public GameObject tutorialLasers;
    public GameObject tutorialTaller;
    public GameObject panelVictory;
    public GameObject toppingPanel;
    public GameObject panelRobot;
    public GameObject panelColor;

    public GameObject WireMenu;

    GameState state;
    public bool sceneSettings = false;
    bool boolRobotPanel = true;

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
                sceneSettings = false;
                state = newState;
                tutorialLasers.SetActive(false);
                settingsMenuPlay.SetActive(false);
                panelVictory.SetActive(false);
                toppingPanel.SetActive(false);
                WireMenu.SetActive(false);
                panelColor.SetActive(false);
                break;
            case GameState.Lasers:
                sceneSettings = false;
                state = newState;
                tutorialLasers.SetActive(true);
                settingsMenuLasers.SetActive(false);
                break;
            case GameState.Settings:
                panelRobot.SetActive(false);
                if (state == GameState.Playing)
                    settingsMenuPlay.SetActive(true);
                else if (state == GameState.Lasers)
                    settingsMenuLasers.SetActive(true);
                sceneSettings = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Menu:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                state = newState;
                break;
            case GameState.Wire:
                panelRobot.SetActive(false);
                WireMenu.SetActive(true);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Topping:
                panelRobot.SetActive(false);
                toppingPanel.SetActive(true);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Color:
                panelColor.SetActive(true);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Playing;
                    else GameManager.Instance.State = GameState.Settings;
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (boolRobotPanel) { panelRobot.SetActive(true); boolRobotPanel = false; }
                    else { panelRobot.SetActive(false); boolRobotPanel = true; }
                }
                break;
            case GameState.Lasers:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Lasers;
                    else GameManager.Instance.State = GameState.Settings;
                }
                break;
            case GameState.Wire:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Wire;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
            case GameState.Topping:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Topping;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
            case GameState.Color:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Color;
                    else GameManager.Instance.State = GameState.Playing;
                }
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
