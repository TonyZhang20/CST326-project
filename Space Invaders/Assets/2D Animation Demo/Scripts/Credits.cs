using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI High;
    public TextMeshProUGUI Your;
    public TextMeshProUGUI Count;


    private float Highest;
    private float YourScore;

    private float m_Timer = 0;
    private float totalTime = 5f;
    public float wait_Time = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Highest = PlayerPrefs.GetFloat("Highest");
        YourScore = PlayerPrefs.GetFloat("YourScore");
        High.text = "Highest Score: " + Highest;
        Your.text = "Your Score is: " + YourScore;
    }

    // Update is called once per frame
    void Update()
    {
        Count.text = "After " + totalTime + " seconds, will back to main menu.";
        m_Timer += Time.deltaTime;
        if (m_Timer > wait_Time)
        {
            totalTime = totalTime - 1;
            m_Timer = 0;
        }
        if (totalTime == -1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
