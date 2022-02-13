using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip pongSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Debug.Log("Hit!");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = pongSound;
            audioSource.Play();
        }

    }
}
