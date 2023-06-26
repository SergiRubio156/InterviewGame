using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RandomImageGenerator : MonoBehaviour
{
    public Texture2D[] images; // Array que contiene las imágenes que quieres mostrar
    public RawImage rawImage; // Referencia al componente RawImage para mostrar las imágenes
    public Texture2D[] pinturas; // Array que contiene las pinturas que quieres mostrar
    public RawImage pinturaImage; // Referencia al componente RawImage para mostrar las pinturas
    public List<TextMeshProUGUI> cpuText;
    public TextMeshProUGUI countText; // Referencia al componente de TextMeshProUGUI para el contador
    public EvaluateManager evaluateManager;

    //pasarAl evaluateManager

    [SerializeField]
    private int idCard;
    public float numDown, numUp;

    private void Awake()
    {
        evaluateManager.RecivedRobotsCards();
    }
    private void Start()
    {
        GenerateNewRobot();
    }

    public int GetIdCard()
    {
        return idCard;
    }
    public void SetIdCard(int _num)
    {
        idCard = _num;
    }
    public void GenerateNewRobot()
    {
        IncrementCount();
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomNumberCPU1();
        GenerateRandomNumberCPU2();
    }


    public void GenerateRandomImage()
    {
        int randomIndex = Random.Range(0, images.Length);
        //randomImage = randomIndex;
        rawImage.texture = images[randomIndex];
    }

    public void GenerateRandomPintura()
    {
        int randomIndex = Random.Range(0, pinturas.Length);
        pinturaImage.texture = pinturas[randomIndex];
    }


    public void IncrementCount()
    {
        countText.text = idCard.ToString();
    }

    public void GenerateRandomNumberCPU1()
    {
        float randomNumber = Random.Range(0f, 2f);
        randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al número más cercano con incremento de 0.5 o 1
        numUp = randomNumber;
        cpuText[0].SetText(randomNumber.ToString());
        
    }
    public void GenerateRandomNumberCPU2()
    {
        float randomNumber = Random.Range(0f, 2f);
        randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al número más cercano con incremento de 0.5 o 1
        numDown = randomNumber;
        cpuText[1].SetText(randomNumber.ToString());
    }
}
