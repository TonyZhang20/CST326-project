using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Ball : MonoBehaviour
{
    public int ForceX = 100;
    public int ForceZ = 1000;
    public float pushing = 1;

    private GameObject paddleOne;
    private GameObject paddleTwo;

    public Material[] myMaterial;

    private Rigidbody rb;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(0, 2);
        rb = GetComponent<Rigidbody>();
        if(randomNumber < 1)
        {
            velocity = new Vector3(ForceX, -1, ForceZ);
            rb.velocity = velocity;
            //rb.AddForce(new Vector3(ForceX, 0, ForceZ));
        }
        else
        {
            velocity = (new Vector3(-ForceX, -1, -ForceZ));
            rb.velocity = velocity;
            //rb.AddForce(new Vector3(-ForceX, 0,-ForceZ));
        }


        //Debug.Log(paddle.GetComponent<Collider>().bounds.size.ToString());
        paddleOne = GameObject.FindGameObjectWithTag("PaddleOne");
        paddleTwo = GameObject.FindGameObjectWithTag("PaddleTwo");

    }



    // Update is called once per frame
    void Update()
    {
        // rb.velocity = velocity;
        //Debug.Log(rb.velocity.ToString());

        if (rb.velocity.x > 0)
        {
            if (rb.velocity.z > velocity.z)
            {
                velocity.z = rb.velocity.z;
            }
            if (rb.velocity.z < velocity.z)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocity.z);
            }
        }

        if (rb.velocity.x < 0)
        {
            if (rb.velocity.z < velocity.z)
            {
                velocity.z = rb.velocity.z;
            }
            if (rb.velocity.z > velocity.z)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocity.z);
            }
        }
        //Debug.Log(this.GetComponent<Rigidbody>().velocity.ToString());

        
        //paddle.transform.localScale = new Vector3(3f,1f,1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            velocity.z = rb.velocity.z;

            if (velocity.z < 0)
                velocity.z -= Random.Range(3.0f, 4.0f);
            else
                velocity.z += Random.Range(3.0f, 4.0f);

         

            float force = this.transform.position.x - other.transform.position.x;

            velocity.x = pushing * force;
            //Debug.Log(velocity.x);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y,velocity.z);
            
            //Debug.Log(force + " " + rb.velocity.ToString());


            //Debug.Log(other.transform.position);

        }

        if(other.gameObject.CompareTag("Wall"))
        {
            Material material = gameObject.GetComponent<Renderer>().material;
            gameObject.GetComponent<Renderer>().material = myMaterial[Random.Range(0, myMaterial.Length)];

            while (gameObject.GetComponent<Renderer>().material == material)
            {
                gameObject.GetComponent<Renderer>().material = myMaterial[Random.Range(0, myMaterial.Length)];
            }

        }

        if(other.gameObject.CompareTag("Suprise"))
        {
            //Debug.Log("hit");
            if(this.GetComponent<Rigidbody>().velocity.z > 0)
            {
                paddleOne.transform.localScale = new Vector3(3f, 1f, 1.5f);
            }
            else if(this.GetComponent<Rigidbody>().velocity.z < 0)
            {
                paddleTwo.transform.localScale = new Vector3(3f, 1f, 1.5f);
            }
            other.gameObject.SetActive(false);
        }
    }

}

