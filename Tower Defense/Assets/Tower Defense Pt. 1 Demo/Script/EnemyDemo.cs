using System.Collections.Generic;
using UnityEngine;

public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties
    //   health, speed, coin worth
    //   waypoints
    //   delegate event for outside code to subscribe and be notified of enemy death
    public float health = 5;
    public float speed = 3f;
    public float coinWorth = 1;

    public GameObject Player;

    public List<Transform> wayPointList;
    public Transform startPoint;

    private Animator animator;
    private int targetWaypointIndex;
    private bool isback = false;
    private bool isfront = false;

    private bool keepMoving = true;

    private Quaternion mRotation;
    // NOTE! This code should work for any speed value (large or small)

    //-----------------------------------------------------------------------------
    void Start()
    {
        // todo #2
        transform.position = startPoint.position;
        mRotation = transform.rotation;
        animator = GetComponent<Animator>();
        targetWaypointIndex = 0;
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        if (targetWaypointIndex < 6)
        {
            animator.SetTrigger("Run");
        }

        if (targetWaypointIndex > 6)
        {
            animator.SetTrigger("RunFast");
            speed = 8;
        }

        //todo #3 Move towards the next waypoint
        if (targetWaypointIndex < wayPointList.Count && keepMoving == true)
        {
            Vector3 targetPosition = wayPointList[targetWaypointIndex].position;
            Vector3 movementDir = (targetPosition - transform.position).normalized;
            transform.position += movementDir * speed * Time.deltaTime;
            Vector3 targetToEnemy =(transform.position - targetPosition).normalized;
            var wayPointLeft = new Vector3( -25f, wayPointList[targetWaypointIndex].position.y, wayPointList[targetWaypointIndex].position.z);
            float dotResult = Vector3.Dot(targetToEnemy, wayPointLeft);
            //Debug.Log(dotResult);
            checkDirection(dotResult);
        }

        if(health <= 0)
        {
            animator.SetTrigger("Death");
            keepMoving = false;
        }

        if(targetWaypointIndex == wayPointList.Count - 1)
        {
            animator.SetTrigger("Win");
        }

        // todo #4 Check if destination reaches or passed and change target
    }

    //-----------------------------------------------------------------------------
    private void TargetNextWaypoint()
    {
    }

    public void takeDamage(float Damage)
    {
        health -= Damage;
    }
    public void Death(float worthCoin)
    {
        Destroy(gameObject);
        Player.GetComponent<PlayerData>().SendMessage("addMoney", coinWorth);
    }

    private void checkDirection(float dotResult)
    {
        if (dotResult < 0)
        {
            if(isback == true)
                transform.rotation = new Quaternion(mRotation.x, mRotation.y, mRotation.z, mRotation.w);
            isback = true;
            if (isfront == true)
            {
                targetWaypointIndex++;
                isback = false;
                isfront = false;
            }
        }
        else if (dotResult > 0)
        {
            if(isfront == true)
                transform.rotation = new Quaternion(mRotation.x, -mRotation.y, mRotation.z, mRotation.w);
            isfront = true;
            if (isback == true)
            {
                targetWaypointIndex++;
                isback = false;
                isfront = false;
            }
        }
        else if (dotResult == 0)
        {
            targetWaypointIndex++;
            isback = false;
            isfront = false;
        }

    }
}
