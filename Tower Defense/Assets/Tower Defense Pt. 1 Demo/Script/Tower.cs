using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{ 
    public GameObject Bullet;
    public float Speed;

    public AudioClip audioClip;

    private List<GameObject> EnemyList = new List<GameObject>();

    private float accumulatedTime = 0f;
    private float totalTime = 0f;

    private ParticleSystem ParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem = transform.Find("Sphere").transform.Find("SmallExplosion").GetComponent<ParticleSystem>();
        ParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> list = new List<GameObject>();

        foreach (var enemy in EnemyList)
        {
            if (enemy == null)
            {
                list.Add(enemy);
            }
        }

        foreach(var enemy in list)
        {
            EnemyList.Remove(enemy);
        }


        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > 1f)
        {
            totalTime -= 1;
            accumulatedTime = 0f;
        }

        if (EnemyList.Count != 0)
        {
            mRotation(EnemyList[0]);
            if (totalTime <= 0.5f)
            {
                totalTime = 2.5f;
                Fire((EnemyList[0]));
                if(ParticleSystem.isStopped)
                {
                    ParticleSystem.Play();
                }
            }
        }


    }

    public void EnterTrigger(GameObject Enemy)
    {
        if(!EnemyList.Contains(Enemy))
        {
            EnemyList.Add(Enemy);
        }
    }

    public void LeaveTrigger(GameObject Enemy)
    {
        EnemyList.Remove(Enemy);
    }

    void mRotation(GameObject target)
    {
        var StartPosition = transform.Find("RayTrigger").position;
        
        if(StartPosition == null)
        {
            Debug.LogError("Do not find RayTrigger");
        }

        var EnemyPosition = target.transform.position;

        var barrel = transform.Find("Sphere");

        barrel.LookAt(EnemyPosition);
    }

    private void Fire(GameObject gameObject)
    {
        var mBullet = Instantiate(Bullet, transform.Find("Fire").position, Quaternion.identity);
        mBullet.GetComponent<Bullet>().GetTarget(EnemyList[0]);
        
        var AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();
    }


}
