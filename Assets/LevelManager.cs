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

    public void RemoveLevel()
    {
        levelPrefabs.RemoveAt(0);
    }
}
