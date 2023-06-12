using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomImageGenerator : MonoBehaviour
{
    public Texture2D[] images; // Array que contiene las imágenes que quieres mostrar
    public RawImage rawImage; // Referencia al componente RawImage para mostrar las imágenes
    public Texture2D[] pinturas; // Array que contiene las pinturas que quieres mostrar
    public RawImage pinturaImage; // Referencia al componente RawImage para mostrar las pinturas
    public Texture2D[] cpu; // Array que contiene las imágenes de CPU que quieres mostrar
    public TextMeshProUGUI countText; // Referencia al componente de TextMeshProUGUI para el contador
    //public Button fetButton; // Referencia al botón
    private string randomNumTextTag = "text"; // Tag del objeto de texto para el número aleatorio

    private int count;
    private int previousCPUIndex = -1; // Índice de la imagen de CPU anterior

    private void Start()
    {
        //fetButton.onClick.AddListener(OnClickFetButton);
        GenerateRandomImage();
        GenerateRandomPintura();
        count = 0;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        {
            //GenerateNewRobot();
        }
    }
    public void GenerateNewRobot()
    {
        IncrementCount();
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomCPU();
        GenerateRandomNumberWithTag();
    }
    private void OnClickFetButton()
    {
        IncrementCount();
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomCPU();
        GenerateRandomNumberWithTag();
    }

    public void GenerateRandomImage()
    {
        int randomIndex = Random.Range(0, images.Length);
        rawImage.texture = images[randomIndex];
    }

    public void GenerateRandomPintura()
    {
        int randomIndex = Random.Range(0, pinturas.Length);
        pinturaImage.texture = pinturas[randomIndex];
    }

    public void GenerateRandomCPU()
    {
        GameObject[] textObjects = GameObject.FindGameObjectsWithTag(randomNumTextTag);

        foreach (GameObject textObject in textObjects)
        {
            RawImage cpuRawImage = textObject.GetComponent<RawImage>();

            if (cpuRawImage != null)
            {
                int randomIndex = GetUniqueRandomCPUIndex();
                cpuRawImage.texture = cpu[randomIndex];
            }
        }
    }

    private int GetUniqueRandomCPUIndex()
    {
        int randomIndex = Random.Range(0, cpu.Length);

        while (randomIndex == previousCPUIndex)
        {
            randomIndex = Random.Range(0, cpu.Length);
        }

        previousCPUIndex = randomIndex;
        return randomIndex;
    }

    public void IncrementCount()
    {
        count++;
        countText.text = count.ToString("D2");
    }

    public void GenerateRandomNumberWithTag()
    {
        TextMeshProUGUI[] randomNumTexts = GameObject.FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in randomNumTexts)
        {
            if (text.CompareTag(randomNumTextTag))
            {
                float randomNumber = Random.Range(1f, 5f);
                randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al número más cercano con incremento de 0.5 o 1
                text.text = randomNumber.ToString("F1");
            }
        }
    }
}
