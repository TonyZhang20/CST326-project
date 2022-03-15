using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public float Speed = 1;
    public Transform shottingOffset;
    // Update is called once per frame
    public float SpeedUpScore = 8f;

    public TextMeshProUGUI HI_ScoreGUI;
    public TextMeshProUGUI ScoreGUI;

    public AudioClip[] PlayerSound;

    private float Score;
    private float HI_Score;

    private GameObject mBullet;

    private Animator PlayerAnimator;

    private void Start()
    {
        Score = 0;
        HI_Score = PlayerPrefs.GetFloat("Highest");
        ShowScore(HI_Score, HI_ScoreGUI);

        PlayerAnimator = GetComponent<Animator>();
    }
    void Update()
    {

        mBullet = GameObject.Find("Bullet(Clone)");

        if (Input.GetKeyDown(KeyCode.Space) && mBullet == null)
        {
            GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
            PlayerAnimator.SetTrigger("Shoot");
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = PlayerSound[0];
            audioSource.Play();
            Destroy(shot, 2f);
         }

        var axis = Input.GetAxis("Horizontal");
        var position = this.transform.position;
        if(position.x <= 8.7f && position.x >= -8.7)
        {
            this.transform.position = new Vector3(position.x + (axis * Time.deltaTime * Speed) , position.y, position.z);
        }
        else if(position.x > 8.7)
        {
            this.transform.position = new Vector3(8.69f, position.y, position.z);
        }
        else if(position.x < -8.7)
        {
            this.transform.position = new Vector3(-8.69f, position.y, position.z);
        }
        
        CheckScore(Score, HI_Score);
        ShowScore(Score, ScoreGUI);

    }

    void CheckScore(float mScore, float HI_Score)
    {
        if(mScore > HI_Score)
        {
            ShowScore(mScore, HI_ScoreGUI);
            HI_Score = mScore;
            PlayerPrefs.SetFloat("Highest", HI_Score);
        }
        else
        {
            PlayerPrefs.SetFloat("YourScore", mScore);
        }
    }

    void ShowScore(float mScore, TextMeshProUGUI textMeshPro)
    {
        if(mScore > 1000)
        {
            textMeshPro.text = mScore.ToString();
        }
        else if(mScore > 100)
        {
            textMeshPro.text = "0" + mScore.ToString();
        }else if(mScore > 10)
        {
            textMeshPro.text = "00" + mScore.ToString();
        }
        else
        {
            textMeshPro.text = "000" + mScore.ToString();
        }
    }

    public void addScore(float mScore)
    {
        Score = Score + mScore;
    }

    public float getScore()
    {
        return Score;
    }

    public float getSpeedUp()
    {
        return SpeedUpScore;
    }

    public void setSpeedUpScore(float data)
    {
        SpeedUpScore = data;
    }

    public void MDestory()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(2);
    }

    public void MPlaySound()
    {
        var AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = PlayerSound[1];
        AudioSource.Play();
    }
}
