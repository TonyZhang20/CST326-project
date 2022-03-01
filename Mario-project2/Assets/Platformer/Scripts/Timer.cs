using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI ShowTheTime;

    private float accumulatedTime = 0f;
    private float totalTime = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        if(accumulatedTime > 1f)
        {
            totalTime -= 1f;
            accumulatedTime = 0f;

            //Debug.Log($"time is {totalTime}");
            ShowTheTime.text = $"Time: {totalTime}";
        }
        if(totalTime <= 0f)
        {
            SceneManager.LoadScene(0);
            Debug.Log("You Failed");
        }
    }

}
