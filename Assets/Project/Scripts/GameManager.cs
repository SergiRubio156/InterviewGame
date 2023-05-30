using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Vamos a crear un Script Manager donde podremos coordinar todos los script 


//Vamos a crear un enum, un enum es como una lista de constantes enteras con nombres que nos sirven
//para poder pasar de fases del juego, podriamos decir que pasar de opciones a jugar es 
//estado del juego
public enum GameState
{
    Playing,
    Lasers,
    Settings,
    Menu,
    Wire,
    Exit


};


public class GameManager : MonoBehaviour
{
    private  static GameManager instance;

    public static GameObject _instance;

    public sceneManager sceneManager;

    
    public List<string> levelLaser = new List<string>() { "NIVEL 1", "NIVEL 2", "NIVEL 3", "NIVEL 4", "NIVEL 5", "NIVEL 6", "NIVEL 7", "NIVEL 8", "NIVEL 9"};


    public static event Action<GameState> OnGameStateChanged;

    private GameState state = GameState.Playing;

    bool lvlCompleted = true;

    int  nameLevel = -1;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Si no hay instancia existente, buscarla en la escena
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    // Si no se encuentra en la escena, crear una nueva instancia
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _instance = this.gameObject;

        sceneManager = GameObject.FindObjectOfType<sceneManager>();

        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        if (gameManagers.Length > 1)
        {
            // Hay más de un objeto GameManager, eliminar los duplicados
            for (int i = 1; i < gameManagers.Length; i++)
            {
                Destroy(gameManagers[i].gameObject);
            }
        }

    }
    void Start() 
    {
        _instance = this.gameObject;       
    }


    public GameState State
    {
        get { return state; }
        set
        {
            Debug.Log("value " + value);
            Debug.Log("State " + state);

            if (state != value)
            {
                //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
                //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

                switch (value)
                {
                    case GameState.Menu:
                        HandleMenu();
                        state = value;
                        break;//break se utiliza para romper el "IF"

                    case GameState.Playing:
                        HandlePlaying();
                        state = value;
                        Debug.Log("State " + state);

                        break;

                    case GameState.Lasers:
                        if (state == GameState.Playing)
                        {
                            state = value;
                            HandlePlayerLasers();
                        }
                        state = value;
                        break;

                    case GameState.Settings:
                        HandleSettings();
                        state = value;
                        break;

                    case GameState.Exit:
                        HandleExit();
                        break;

                    case GameState.Wire:
                        HandleSettings();
                        state = value;
                        break;
                    default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);//pone el valor "newState" a null para que no pete el programa.
                }
            }
            OnGameStateChanged?.Invoke(state);//Esta linia comprueba si el estado ha cambiado y si es true entonces va a todos los scripts y cambia el estado.
        }
    }

    private void HandleExit()
    {
        Application.Quit();
    }


    private string NameLevel()
    {
        nameLevel++;
        Debug.Log(nameLevel);
        return levelLaser[nameLevel];
    }
    private void HandlePlayerLasers()//string _name)
    {
        sceneManager.ChangeSceneLevel(NameLevel());
    }

    private void HandleMenu()
    {
        sceneManager.ChangeScene("Menu");
    }

    private void HandleSettings()
    {
        
    }

    private void HandlePlaying()
    {
        sceneManager.ChangeScene("Pruebas2");
    }

    public void LvlCompleted()
    {
        lvlCompleted = true;
    }
    public bool OpenDoor()
    {
        if (lvlCompleted)
        {
            return true;
        }
        return false;
    }

}