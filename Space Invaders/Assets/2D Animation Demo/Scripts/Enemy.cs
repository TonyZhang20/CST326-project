using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;

    private GameObject mbullet;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        mbullet = GameObject.Find("EnemyBullet(Clone)");
        if(mbullet == null)
        {
            GameObject shot = Instantiate(bullet, this.transform.position, Quaternion.identity);
            Destroy(shot, 2f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("You Lose!");
        }
    }
}
