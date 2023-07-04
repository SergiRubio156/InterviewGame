using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public float tiempomax = 8;
    public float minutos = 0;
    public float segundos = 60;
    public TextMeshProUGUI texto;
    public int ammountLaserPuzzleSkipped = 0;
    public KeyCode keyToSkipPuzzle = KeyCode.M;

    // Contador del tiempo hasta entre "M"
    public float puzzleSkippedCont = 0;
    public bool startPuzzleCont = false;
    public List<float> timePuzzleCont = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToSkipPuzzle))
        {
            IncreaseSkippedAmmount();
        }
        
        if ((int)segundos == 0)
        {
            tiempomax--;
            segundos = 59; 
        } 
        else
        {
            segundos -= Time.deltaTime;
        }
        if(startPuzzleCont)
        {
            puzzleSkippedCont += Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchPuzzleCounterBool(true);
        }
        texto.text = " Tiempo: " + tiempomax.ToString() + ":" + segundos.ToString("#.");
    }
    public void IncreaseSkippedAmmount()
    {
        ammountLaserPuzzleSkipped++;
        timePuzzleCont.Add(puzzleSkippedCont);
        puzzleSkippedCont = 0;
    }
    public void SwitchPuzzleCounterBool(bool state)
    {
        startPuzzleCont = state;
    }
}
