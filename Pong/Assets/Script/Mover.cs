using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{

    public float movementPerSecond = 1f;
    public AudioClip pongSound;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementAxis = Input.GetAxis("Vertical");
        Vector3 force = Vector3.right * movementAxis * movementPerSecond * Time.deltaTime;
        
        Rigidbody rBody = GetComponent<Rigidbody>();
        if(rBody != null)
        {
            rBody.AddForce(force,ForceMode.VelocityChange);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = pongSound;
            audioSource.Play();
        }

    }




}
