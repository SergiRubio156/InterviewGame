using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorito : MonoBehaviour
{

    public float positionX = 17.87f;
    public float speed = 2f;
    public float positionY = 29.23f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector2(positionX, positionY);
    }

    // Update is called once per frame
    void Update()
    {
        positionX -= speed * Time.deltaTime;

        positionY -= speed * Time.deltaTime;

        this.transform.position = new Vector2(positionX, positionY);
    }
}
