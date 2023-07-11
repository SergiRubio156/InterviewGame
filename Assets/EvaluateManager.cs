using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Importa el namespace System.Linq para usar la función Any()

public class EvaluateManager : MonoBehaviour
{
    //RobotEspeficicaciones
    public List<RandomImageGenerator> RobotCards;
    public static int totalId = 0;

    ObjectManager objectManager;

    //Robot que le llega
    float numUp, numDown;
    float robotParts;
    int idRobot;
    Color colorRobot;
    int minutesRobot;
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
                totalId++;
                RobotCards.Add(components[i]);
                RobotCards[totalId - 1].SetIdCard(totalId - 1);
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
                ColorRobot(component.ColorId());
                MinutesRobot(component.numberClock);
            }
        }
    }
    void MinutesRobot(int _num)
    {
        if (minutesRobot == _num)
        {
            Debug.Log("Time Correct");
        }
        else { Debug.Log("Time Incorrect"); }
    }

    void RobotParts(float _num)
    {
        if (robotParts == _num)
        {
            Debug.Log("Robot Correcto");
        }
        else { Debug.Log("Robot Incorrecta"); }
    }
    void ColorRobot(Color _color)
    {
        if (colorRobot == _color)
        {
            Debug.Log("Color Correcto");
        }
        else { Debug.Log("Color Incorrecta"); }
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
            Objects _obj = obj.gameObject.GetComponent<Objects>();
            idRobot = _obj.id;
            numUp = _obj.numUp;
            numDown = _obj.numDown;
            float robotUp = _obj.robotUp;
            float robotDown = _obj.robotDown;
            minutesRobot = _obj.timeColor;
            colorRobot = _obj.FinalColor;
            robotParts = robotUp + robotDown;
            FindId();
            RandomImageGenerator.instance.GenerateNewRobot();

            objectManager.RemoveRobotsList(_obj);
            Destroy(obj.gameObject);
            
        }

    }

}
