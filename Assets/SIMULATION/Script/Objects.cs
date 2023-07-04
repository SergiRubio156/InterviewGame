using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[System.Serializable]
public class Objects : ObjectManager
{

    public int id;
    public GameObject name;
    public ObjectState state;
    public float numUp;
    public float numDown;


    public BoxCollider boxColliderUp;
    public Rigidbody rb;

    public bool cablesCheck = false;
    public bool toppingCheck = false;
    public bool canMove = false;
    public bool colorChoose = false;
    bool isPlaying;
    //COLOR

    public bool colorCheck = false;
    float durationColor = 60f;
    public Gradient gradient;
    float time = 0f;
    public Renderer[] rend = new Renderer[1];
    public GameObject parts;
    public BoxCollider boxColliderDown;

    [SerializeField]
    private Color currentColor;
    public Material outline;


    private void Start()
    {
        name = this.gameObject;
        boxColliderUp = GetComponent<BoxCollider>();
        rend = GetComponentsInChildren<Renderer>();
        parts = transform.GetChild(1).gameObject;
        boxColliderDown = parts.GetComponent<BoxCollider>();
        outline = GetComponentInChildren<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        parts.SetActive(false);

    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
    private void HandleGameStateChanged(GameState newState)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        switch (newState)
        {
            case GameState.Playing:
                isPlaying = true;
                break;
            case GameState.Lasers:
                break;
            case GameState.Settings:
                isPlaying = false;
                break;
            case GameState.Menu:
                isPlaying = false;
                break;
            case GameState.Wire:
                break;
            case GameState.Topping:
                break;
            case GameState.Color:
                isPlaying = false;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }
    public override void ObjectNoTaked()
    {
        boxColliderUp.enabled = true;
        if (parts.activeSelf)
            boxColliderDown.enabled = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        outline.SetFloat("_Outline_Thickness", 0.01f);
    }

    public override void ObjectTaked()
    {
        boxColliderUp.enabled = false;
        if (parts.activeSelf)
            boxColliderDown.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        colorCheck = false;
    }
    public override void ObjectCables()
    {
        if (!cablesCheck)
        {
            StartCoroutine(WaitWire());
        }
    }
    public override void ObjectToppings()
    {
        if (!toppingCheck)
        {
            toppingCheck = true;
            StartCoroutine(WaitToppings());
        }
    }

    public override void ObjectColors()
    {
        boxColliderUp.enabled = true;
        StartCoroutine(WaitColor());
    }
    IEnumerator WaitToppings()
    {
        yield return new WaitForSeconds(0.2f);
        boxColliderUp.enabled = true;
        GameManager.Instance.State = GameState.Topping;

    }
    IEnumerator WaitWire()
    {
        yield return new WaitUntil(() => canMove);
        parts.SetActive(true);
        boxColliderUp.enabled = true;
        GameManager.Instance.State = GameState.Wire;

    }
    private IEnumerator WaitColor()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.State = GameState.Color;

    }

    private void Update()
    {
        if(colorCheck && isPlaying)
        {
            LerpPosition();
        }
    }

    private void LerpPosition()
    {
        currentColor = gradient.Evaluate(time / durationColor);

        time += Time.deltaTime;


        // Combinar el color del gradiente y el color de la textura
        rend[0].materials[1].color = currentColor;
        rend[1].materials[1].color = currentColor;
    }

}
