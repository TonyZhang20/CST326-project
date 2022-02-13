using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Score : MonoBehaviour
{
    public int AddScore = 0;
    
    public TextMeshProUGUI ScoreText;
    public string player;
    public bool isPlayerOne;

    private float myTime;
    private int myScore;

    // Start is called before the first frame update
    void Start()
    {
        myScore = 0 + AddScore;
        SetScoreText();
        if(isPlayerOne)
        {
            player = "Player1: ";
        }
        else
        {
            player = "Player2: ";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetScoreText()
    {
        ScoreText.text = player.ToString() + myScore.ToString();
        int rand = Random.Range(0, 4);

        if (rand == 1)
        {
            ScoreText.color = Color.red;
        }
        else if (rand == 2)
        {
            ScoreText.color = Color.blue;
        }
        else if(rand == 3)
        {
            ScoreText.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    if (this.gameObject.tag == "Player1")
        {
            myScore++;
            SetScoreText();
            Debug.Log(player.ToString() + " get 1 score, now score is " + myScore);
        }


    if (myScore == 11)
        {
           Debug.Log(player.ToString() + " Win!");
           Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }



}
