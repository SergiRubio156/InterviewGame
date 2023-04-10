using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public IslandManager islandManager;
    public List<GameObject> objectsList = new List<GameObject>();

    public  Vector3 positionObject;
    public float speed;

    NavMeshAgent agent;

    
    public bool isMoving = false;
    public bool checkObjects = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (objectsList.Count != 0)
                positionObject = objectsList[0].transform.position;
        }
        if (positionObject != Vector3.zero)
        {
            MoveObject(positionObject);
            isMoving = true;
        }
    }

    public void SendList(List<GameObject> myList)
    {
        foreach (GameObject item in myList)
        {
            if (!objectsList.Contains(item))
                objectsList.Add(item);
        }
    }

    void MoveObject(Vector3 _positionObject)
    {
        agent.destination =  Vector3.Lerp(transform.position, _positionObject, speed * Time.deltaTime);
    }

    public void FinishAction(bool _bool,GameObject _object)
    {
        isMoving = _bool;
        objectsList.Remove(_object);
        positionObject = Vector3.zero;
    }
}
