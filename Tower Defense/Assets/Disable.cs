using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    private float totalTime = 2f;
    private float cumulativeTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cumulativeTime += Time.deltaTime;
        if(cumulativeTime > 1f)
        {
            totalTime -= 1f;
            cumulativeTime = 0f;
        }
        if(cumulativeTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
