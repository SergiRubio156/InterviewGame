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
}
