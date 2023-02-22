using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public AudioClip[] audioClip;
    public AudioSource audioSource;
    private int puntos;
    bool m_Play;
    bool m_ToggleChange;
    public Sprite[] sprites;

    public Image image;

    void Start()
    {
        image.sprite = sprites[0];
        audioSource = GetComponent<AudioSource>();

        m_Play = true;

    }

    private void Update()
    {
        image.sprite = sprites[puntos];
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GoodObjects")
        {
            ++puntos;
            audioSource.PlayOneShot(audioClip[0]);
            DestroyObject(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BadObjects")
        {
            audioSource.PlayOneShot(audioClip[1]); 
            if (puntos != 0)
            --puntos;
            DestroyObject(collision.gameObject);
        }
    }
}
