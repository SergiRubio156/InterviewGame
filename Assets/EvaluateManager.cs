using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Importa el namespace System.Linq para usar la función Any()

public class EvaluateManager : MonoBehaviour
{
    //RobotEspeficicaciones
    public List<RandomImageGenerator> RobotCards;
    public static int totalId = 1;

    ObjectManager objectManager;

    //Robot que le llega
    float numUp, numDown;
    float robotParts;
    int idRobot;

    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();

    }
    public void RecivedRobotsCards()
    {
        RandomImageGenerator[] components = FindObjectsOfType<RandomImageGenerator>();

        for (int i = 0; i < components.Length; i++)
        {
            // Verificar si ya existe un componente con el mismo nombre en el array RobotCards
            if (!RobotCards.Any(card => card.name == components[i].name))
            {
                // Si no existe, agrega el componente al array RobotCards
                RobotCards.Add(components[i]);
                RobotCards[totalId - 1].SetIdCard(totalId - 1);
                totalId++;
            }
        }
    }

    void FindId()
    {
        foreach (RandomImageGenerator component in RobotCards)
        {

            if (idRobot == component.GetIdCard())
            {
                CompareNumUp(component.numUp);
                CompareNumDown(component.numDown);
                RobotParts(component.RobotId());
            }
        }
    }
    void RobotParts(float _num)
    {
        if (robotParts == _num)
        {
            Debug.Log("Robot Correcto");
        }
        else { Debug.Log("Robot Incorrecta"); }
    }
    void CompareNumUp(float _num)
    {
        if(numUp == _num)
        {
            Debug.Log("CPU Correcta");
        }
        else { Debug.Log("CPU Incorrecta"); }
    }
    void CompareNumDown(float _num)
    {
        if (numDown == _num)
        {
            Debug.Log("Memoria Correcta");
        }
        else { Debug.Log("Memoria Incorrecta"); }
    }


    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Interactable"))
        {
            idRobot = obj.gameObject.GetComponent<Objects>().id;
            numUp = obj.gameObject.GetComponent<Objects>().numUp;
            numDown = obj.gameObject.GetComponent<Objects>().numDown;
            float robotUp = obj.gameObject.GetComponent<Objects>().robotUp;
            float robotDown = obj.gameObject.GetComponent<Objects>().robotDown;
            robotParts = robotUp + robotDown;
            FindId();
            RandomImageGenerator.instance.GenerateNewRobot();

            Objects _obj = obj.gameObject.GetComponent<Objects>();
            objectManager.RemoveRobotsList(_obj);
            Destroy(obj.gameObject);
            
        }

    }

}
