using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levelPrefabs;

    public void ChooseLevel()
    {
      
        GameObject objectHijo = Instantiate(levelPrefabs[0], gameObject.transform, true);
    }

    public void RemoveLevel(string _name)
    {
        foreach (GameObject elemento in levelPrefabs)
        {
            if (elemento.name == _name)
            {
                levelPrefabs.RemoveAll(obj => obj.name == _name);
                break;
            }
        }

        GameObject child = gameObject.transform.GetChild(0).gameObject;

        if (child != null)
        {
            Destroy(child);
        }
    }

    public string GetNameLevel()
    {
        return levelPrefabs[0].name;
    }
}
