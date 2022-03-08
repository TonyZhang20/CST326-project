using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool RightSide = false;
    // Start is called before the first frame update
    private float m_Timer = 0;
    public float wait_Time = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > wait_Time)
        {
            Movement();
            m_Timer = 0;
        }
    }

    void Movement()
    {
        var position = this.transform.position;
        if(RightSide == false)
        {
            this.transform.position = new Vector3(position.x + 1, position.y, position.z);
        }
        else
        {
            this.transform.position = new Vector3(position.x - 1, position.y, position.z);
        }

        if(position.x > 6.8f)
        {
            this.transform.position = new Vector3(6.8f, position.y - 0.5f, position.z);
            RightSide = true;
        }

        if(position.x < -7.2f)
        {
            RightSide = false;
            this.transform.position = new Vector3(-7.2f, position.y - 0.5f, position.z);
        }
    }
    public float getWaitTime()
    {
        return wait_Time;
    }
    public void setWaitTime(float WaitTime)
    {
        wait_Time = WaitTime;
    }
}
