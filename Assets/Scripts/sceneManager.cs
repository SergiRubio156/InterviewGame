using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public string GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();        
        return currentScene.name;
    }


    // Update is called once per frame
    public void ChangeScene(string _name)
    {

        if (_name != GetCurrentSceneName())
        {
            switch (_name)
            {
                case "Menu":
                    SceneManager.LoadScene(_name);

                    break;
                case "Play":
                    SceneManager.LoadScene(_name);

                    break;

            }
        }
    }

    public void ChangeSceneLevel(string _name)
    {

        if (_name != GetCurrentSceneName())
        {
            switch (_name)
            {
                case "NIVEL 1":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 2":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 3":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 4":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 5":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 6":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 7":
                    SceneManager.LoadScene(_name);
                    break;

                case "NIVEL 8":
                    SceneManager.LoadScene(_name);
                    break;
                case "NIVEL 9":
                    SceneManager.LoadScene(_name);
                    break;

            }
        }
    }
}
