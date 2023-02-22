using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();


    public float speed = 8f;
    private float positionX;
    private float timeRemaining = 0f;
    public float time = 5f;

    bool direction = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            positionX += speed * Time.deltaTime;

            this.transform.position = new Vector2(positionX, 0);
        }
        else if(!direction)
        {
            positionX -= speed * Time.deltaTime;

            this.transform.position = new Vector2(positionX, 0);
        }
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime * 2;
        else
        {
            timeRemaining = Random.Range(2, 5); 
            respawnObject();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "respawn")
        direction =! direction;
    }


    public void respawnObject()
    {
        int number = Random.Range(0, 4);
        Instantiate(Objects[number],this.transform.position,Quaternion.identity);
    }

    
}
