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
                case "Level1":
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
                case "Level1":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level2":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level3":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level4":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level5":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level6":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level7":
                    SceneManager.LoadScene(_name);
                    break;

                case "Level8":
                    SceneManager.LoadScene(_name);
                    break;

            }
        }
    }
}
