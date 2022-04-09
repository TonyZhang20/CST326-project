using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public float Economy;
    public TextMeshProUGUI textMeshProUGUI;

    public float health;

    public TextMeshProUGUI tHealth;
    public TextMeshProUGUI Money;

    private float totalTime = 3;
    private float accumulativeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        showHealth();
        showMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            accumulativeTime += Time.deltaTime;
            if (accumulativeTime > 1f)
            {
                totalTime -= 1;
                accumulativeTime = 0;
            }
            if(totalTime <= 0)
            {
                lose();
            }
        }
    }

    public void addMoney(float coin)
    {
        Economy += coin;
        if (textMeshProUGUI != null)
            textMeshProUGUI.text = "Coin: " + Economy;
        showMoney();
    }

    public void buildTower(float spend)
    {
        Economy -= spend;
        showMoney();
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        showHealth();
    }

    private void lose()
    {
        //change scene to Lose or have TMP to show lose canvas;
        SceneManager.LoadScene(2);
    }

    public float GetMoney()
    {
        return Economy;
    }

    void showMoney()
    {
        Money.text = "Money: " + Economy;
    }

    void showHealth()
    {
        tHealth.text = "Health: " + health;
    }
}
