using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HavingRay : MonoBehaviour
{
    private TextMeshProUGUI TextMeshProUGUI;

    private Ray ray;
    private RaycastHit hit;
    private GameObject Object;

    private int coin;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Object = hit.collider.gameObject;
                if(Object.tag == "QuestionBox")
                {
                    TextMeshProUGUI = GameObject.Find("Canvas/Coin").GetComponent<TextMeshProUGUI>();
                    
                    if(TextMeshProUGUI == null)
                    {
                        Debug.Log("Error,TextMesh brick is null");
                    }

                    coin = coin + 1;
                    if(coin < 10)
                    {
                        TextMeshProUGUI.text = "x0" + coin;
                    }
                    else
                    {
                        TextMeshProUGUI.text = "x" + coin;
                    }

                    TextMeshProUGUI = GameObject.Find("Canvas/Mario").GetComponent<TextMeshProUGUI>();
                    SetScore(TextMeshProUGUI, 100);
                }

                if(Object.tag == "Brick")
                {
                    TextMeshProUGUI = GameObject.Find("Canvas/Mario").GetComponent<TextMeshProUGUI>();
                    SetScore(TextMeshProUGUI, 100);
                    Destroy(Object);
                }

                
            }
        }

        if(transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90f, 0);
        }
    }

    void SetScore(TextMeshProUGUI Text, int AddNum)
    {
        score = score + AddNum;
        int count = score;
        int digits = 0;

        while(count/10 > 0)
        {
            digits++;
            count /= 10;
        }

        string TextDigits = "MARIO\n";
        for(int i = 0; i < (6 - digits); i++)
        {
            TextDigits = TextDigits + "0";
        }
        TextDigits = TextDigits + score.ToString();
        Text.text = TextDigits;
    }
}
