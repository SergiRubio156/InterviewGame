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
    Colors
};

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private List<Objects> objectList = new List<Objects>();
    private List<Objects> a = new List<Objects>();
    ObjectState State = ObjectState.NoTaked;

    // Start is called before the first frame update
    void Start()
    {
        objectList = UnityEngine.Object.FindObjectsOfType<Objects>().ToList();//GameObject.FindGameObjectsWithTag("Interactable").ToList();

        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].id = i;
        }
    }

    
    public int GetObjectPositionInList(GameObject _obj)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].name == _obj)
                return i;
        }
        return -1;
    }

    /*public void ObjectGameState(int _index, ObjectState newState)
    {
        State = objectList[_index].state;
        objectList[_index].state = newState;

        if (State != newState)
        {
            //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
            //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

            switch (newState)
            {
                case ObjectState.NoTaked:
                    ObjectNoTaked();
                    State = newState;
                    break;//break se utiliza para romper el "IF"

                case ObjectState.Taked:
                    ObjectTaked();
                    State = newState;
                    break;

                case ObjectState.Toppings:
                    ObjectToppings();//NameLevel(State));
                    State = newState;
                    break;

                case ObjectState.Cables:
                    ObjectCables();
                    State = newState;
                    break;

                case ObjectState.Colors:
                    ObjectColors();
                    break;
                default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                    Debug.Log("Error");
                    break;
                    //throw new ArgumentOutOfRangeException(nameof(newState), newState, null);//pone el valor "newState" a null para que no pete el programa.
            }
        }
        //OnGameStateChanged?.Invoke(newState);//Esta linia comprueba si el estado ha cambiado y si es true entonces va a todos los scripts y cambia el estado.
                                             //Debug.Log("State " + State);
                                             //Debug.Log("Newstate " + newState);

    }*/

    /*public override void ObjectNoTaked()
    {
        base.ObjectNoTaked();
    }

    public override void ObjectTaked()
    {
        base.ObjectTaked();
    }

    void ObjectToppings()
    {
        Debug.Log(gameObject.name + " toppings");
    }

    public override void ObjectCables()
    {
        base.ObjectCables();
    }
    void ObjectColors()
    {
        Debug.Log(gameObject.name + " colors");
    }*/
}
