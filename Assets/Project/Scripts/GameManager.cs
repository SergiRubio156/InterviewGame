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
    public static GameManager _instance;

    public GameObject[] managers =  new GameObject[] { null,null};

    public static GameObject instance;

    public sceneManager sceneManager;

    
    List<string> levelLaser = new List<string>() { "NIVEL 1", "NIVEL 2", "NIVEL 3", "NIVEL 4", "NIVEL 5", "NIVEL 6", "NIVEL 7", "NIVEL 8", "NIVEL 9"};


    public static event Action<GameState> OnGameStateChanged; 

    GameState State = GameState.Playing;

    bool lvlCompleted = true;

    int  nameLevel = -1;

    public static GameManager Instance
    {    
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is Null!!!!"); 
            return _instance;
        }


    }

    void Awake() //El awake se entra igualmente que el script esta desactivado
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager");
        sceneManager = GetComponentInChildren<sceneManager>();
        
    }

    void Start() 
    {
        UpdateGameState(GameState.Playing);
        _instance = this;
        DontDestroyOnLoad(this.gameObject); 
       
    }


    public void UpdateGameState(GameState newState)
    {
        if (State != newState)
        {
            //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
            //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

            switch (newState)
            {
                case GameState.Menu:
                    HandleMenu();
                    State = newState;
                    break;//break se utiliza para romper el "IF"

                case GameState.Playing:
                    if (State == GameState.Wire)
                    {

                    }
                    HandlePlaying();
                    State = newState;
                    break;

                case GameState.Lasers:
                    HandlePlayerLasers();//NameLevel(State));
                    State = newState;
                    break;

                case GameState.Settings:
                    HandleSettings();
                    State = newState;
                    break;

                case GameState.Exit:
                    HandleExit();
                    break;
                case GameState.Wire:
                    HandleSettings();
                    break;
                default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);//pone el valor "newState" a null para que no pete el programa.
            }
        }
        OnGameStateChanged?.Invoke(newState);//Esta linia comprueba si el estado ha cambiado y si es true entonces va a todos los scripts y cambia el estado.
        //Debug.Log("State " + State);
        //Debug.Log("Newstate " + newState);
        
    }

    private void HandleExit()
    {
        Application.Quit();
    }


    private string NameLevel(GameState _newState)
    {
        /*if (_newState != GameState.Settings)
        {
            Debug.Log(nameLevel);
            nameLevel++;
        }*/
        return "NIVEL 1";//levelLaser[nameLevel];
    }
    private void HandlePlayerLasers()//string _name)
    {
        sceneManager.ChangeSceneLevel("NIVEL 1");
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