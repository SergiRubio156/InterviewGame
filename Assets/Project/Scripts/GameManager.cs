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
    Exit


};

public class GameManager : MonoBehaviour
{
    public static GameManager _instance; //Creamos una variable en Static para que todos los otros scripts puedan utilizar este script

    public GameObject[] managers =  new GameObject[] { null,null};

    public static GameObject instance;

    public sceneManager sceneManager;

    //un evento static esta disponible para todos los scripts en cualquier momento, incluso si no existe ninguna instancia de la clase
    //Nosotros utilizaremos el evento para decir que si estas en GameState.menu el objeto PanelMenu se active, un evento te permite comparar scripts con Unity.GameObjects
    public static event Action<GameState> OnGameStateChanged; 

    GameState State = GameState.Playing;

    bool ejecutar = true;

    string nameLevel;


    //Una funcion statica, es una funcion que nos permite llamar al codigo sin necesidad de una instancia, en palabras pa tontos
    //El script GameManager con todas sus variables y funciones (que aparezcan en public) apareceran en todos los scripts, eso nos
    //permitira llamarlos quando sea necesario.
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is Null!!!!"); //Si sale esto es que el GameManager no funciona
            return _instance;
        }


    }

    void Awake() //El awake se entra igualmente que el script esta desactivado
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager");
        sceneManager = GetComponentInChildren<sceneManager>();
        _instance = this;
    }

    void Start() //Solo se entra una vez, pero si el script esta desactivado no entra
    {
        UpdateGameState(GameState.Playing);//Entro en la funcion UpdateGameState, y ponemos como referencia el GameState.Menu porque es el stado que queremos
        
        DontDestroyOnLoad(this.gameObject); //Esto lo que hace es que no se destruya lobjeto quando se cambia de escena
        if (managers[1] != null)
           Destroy(managers[1].gameObject); //!PREGUNTAR PROFESOR, PORK APARECEN GAMEMANAGER CADA VEZ QUE SE ENTRA A UNA SCENA NUEVA, Y KIERO ELIMINARLO, HE PODIDO ELIMINARLO PERO ME PONE GAMEMANAGER NULL PERO AUN ASI SIGUE FUNCIONANDO
    }

    // Update is called once per frame
    void Update(){}

    public void UpdateGameState(GameState newState)
    {
        if (State != newState)
        {
            //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
            //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

            switch (newState)
            {
                case GameState.Menu:
                    HandleSelectButton();
                    State = newState;
                    break;//break se utiliza para romper el "IF"

                case GameState.Playing:
                    HandlePlaying();
                    State = newState;
                    break;

                case GameState.Lasers:
                    HandlePlayerLasers(nameLevel);
                    State = newState;
                    break;

                case GameState.Settings:
                    HandleSettings();
                    State = newState;
                    break;

                case GameState.Exit:
                    HandleExit();
                    break;
                default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);//pone el valor "newState" a null para que no pete el programa.
            }
        }
        OnGameStateChanged?.Invoke(newState);//Esta linia se utiliza en que si algun otro script se cambia el estado del juego (si pasa de Menu a Playing) se invocara este linia 
        //y volvera a entrar al switch y se cambiara de estado.
        Debug.Log("State " + State);
        Debug.Log("Newstate " + newState);
        
    }

    private void HandleExit()
    {
        Application.Quit();
    }

    private void HandlePlayerLasers(string _name)
    {
        sceneManager.ChangeSceneLevel(_name);
    }

    private void HandleSelectButton()
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

    public void ChooseLevels(string _name)
    {
        nameLevel = _name;
    }
}