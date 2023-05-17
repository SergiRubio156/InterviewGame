using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 

public enum ObjectState
{
    NoTaked,
    Taked,
    Toppings,
    Cables,
    Color
};

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private static List<Objects> objectList = new List<Objects>();

    ObjectState State = ObjectState.NoTaked;

    // Start is called before the first frame update
    void Start()
    {
        objectList = UnityEngine.Object.FindObjectsOfType<Objects>().ToList();
        name = this.gameObject.name;

        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].state = State;
            objectList[i].id = i;
        }
    }

    
    public int GetObjectPositionInList(GameObject _obj)
    {
        if (_obj.GetComponent<Objects>() != null)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                if (objectList[i].name == _obj.GetComponent<Objects>().name)
                    return i;
            }
        }
        return -1;
    }

    public ObjectState GetObjectStateInList(GameObject _obj)
    {
        _obj = _obj.GetComponent<Objects>().name;
        for (int i = 0; i < objectList.Count; i++)
        {
                return objectList[i].state;
        }
        return objectList[0].state;
    }

    public bool GetObjectBoolInList(int i , string _name)
    {
        switch (_name)
        {
            case "Color":
                return objectList[i].colorCheck;

            case "Toppings":
                return objectList[i].toppingCheck;

            case "Wire":
                Debug.Log("!");
                return objectList[i].cablesCheck;

            default:
                return true;
        }

    }

    public void ObjectGameState(int _index, ObjectState newState)
    {
        State = objectList[_index].state;
        objectList[_index].state = newState;

        if (State != newState)
        {
            switch (newState)
            {
                case ObjectState.NoTaked:
                    ObjectNoTaked();
                    State = newState;
                    break;

                case ObjectState.Taked:
                    ObjectTaked();
                    State = newState;
                    break;

                case ObjectState.Toppings:
                    ObjectToppings();
                    State = newState;
                    break;

                case ObjectState.Cables:
                    ObjectCables();
                    State = newState;
                    break;

                case ObjectState.Color:
                    ObjectColors();
                    break;
                default:
                    Debug.Log("Error");
                    break;
                   
            }
        }
        //OnGameStateChanged?.Invoke(newState);//Esta linia comprueba si el estado ha cambiado y si es true entonces va a todos los scripts y cambia el estado.
                                             //Debug.Log("State " + State);
                                             //Debug.Log("Newstate " + newState);

    }

    public virtual void ObjectNoTaked()
    {
    }

    public virtual void ObjectTaked()
    {
        
    }

    void ObjectToppings()
    {
        Debug.Log(gameObject.name + " toppings");
    }

    public virtual void ObjectCables()
    {
       
    }
    public virtual void ObjectColors()
    {
    }
}
