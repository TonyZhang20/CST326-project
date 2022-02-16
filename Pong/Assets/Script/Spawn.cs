using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Spawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform[] spawnTransform;


    private GameObject paddleOne;
    private GameObject paddleTwo;
    private GameObject Suprise;
    private GameObject SpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();

        paddleOne = GameObject.FindGameObjectWithTag("PaddleOne");
        paddleTwo = GameObject.FindGameObjectWithTag("PaddleTwo");
        Suprise = GameObject.FindGameObjectWithTag("Suprise");
        SpeedUp = GameObject.FindGameObjectWithTag("Speed up");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBall()
    {
        int RandomNum = Random.Range(0, spawnTransform.Length);
        Transform radomtransform = spawnTransform[RandomNum];
        GameObject instanse = Instantiate(ballPrefab);
        instanse.GetComponent<Transform>().position = radomtransform.position;
        instanse.transform.position = radomtransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);

            paddleOne.transform.localScale = new Vector3(6f, 1f, 1.5f);
            paddleTwo.transform.localScale = new Vector3(6f, 1f, 1.5f);

            SpawnBall();

            Suprise.SetActive(true);
            SpeedUp.SetActive(true);
        }
    }
}
