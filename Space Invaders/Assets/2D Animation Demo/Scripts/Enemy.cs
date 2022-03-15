using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public bool allowShoot = true;

    public AudioClip[] SoundEffect;

    private GameObject mbullet;
    private GameObject player;

    private Animator Fire;

    private void Start()
    {
        player = GameObject.Find("Player");
        Fire = GetComponent<Animator>();
    }

    private void Update()
    {
        mbullet = GameObject.Find("EnemyBullet(Clone)");
        if(mbullet == null && allowShoot == true)
        {
            GameObject shot = Instantiate(bullet, this.transform.position, Quaternion.identity);
            Fire.SetTrigger("Fire");
            var AudioSource = GetComponent<AudioSource>();
            AudioClip clip = SoundEffect[0];
            AudioSource.Play();
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

    public void MDestroy()
    {
        Destroy (this.gameObject);
    }

    public void PlaySound()
    {

        var AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = SoundEffect[1];
        AudioSource.Play();
    }
}
