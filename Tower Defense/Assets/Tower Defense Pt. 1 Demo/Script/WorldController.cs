using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WorldEnemy;
    public float EnemyNumber;
    public Transform startPosition;

    private GameObject levelEnemy;

    private float accumulatedTime = 0f;
    private float totalTime = 3f;

    void Start()
    {
        levelEnemy = Instantiate(WorldEnemy, startPosition.position, Quaternion.identity) as GameObject;

        Destroy(WorldEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime > 3f && EnemyNumber > 0)
        {
            totalTime -= 1f;
            accumulatedTime = 0f;
            GeneraEnemy();
        }
        if (totalTime <= 0f && EnemyNumber > 0)
        {
            totalTime = 3f;
        }

    }

    void GeneraEnemy()
    {
        EnemyNumber = EnemyNumber - 1;
        var mEnemy = Instantiate(levelEnemy, startPosition.position, Quaternion.identity);
        mEnemy.SetActive(true);
    }
}
