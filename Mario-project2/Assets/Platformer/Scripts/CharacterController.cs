using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class CharacterController : MonoBehaviour
{
    public float runForce = 50f;
    public float maxRunSpeed = 6f;
    public float jumpForce = 20f;
    public float jumpBonus = 3f;

    public bool isJumping = false;

    public float RayLength = 10f;

    private Rigidbody body;
    private Collider collider;

    private Vector3 startPosition;
    private Animator animComp;

    private float BootsSpeed;
    private GameObject Object;

    private TextMeshProUGUI TextMeshProUGUI;
    private int coin;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animComp = GetComponent<Animator>();
        BootsSpeed = maxRunSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float castDistance = collider.bounds.extents.y + 0.1f;
        isJumping = Physics.Raycast(transform.position, Vector3.down, castDistance);

        float axis = Input.GetAxis("Horizontal");
        var face = body.rotation.y;

        if (axis < 0 && face > 0)
        {
            body.rotation = new Quaternion(body.rotation.x, -face, body.rotation.z, body.rotation.w);
        }
        else if(axis > 0 && face < 0)
        {
            body.rotation = new Quaternion(body.rotation.x, -face, body.rotation.z, body.rotation.w);
        }

        body.AddForce(Vector3.right * axis * runForce, ForceMode.Force);

        if(Input.GetKeyDown(KeyCode.Space) && isJumping)
        {
            startPosition = transform.position;
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if(Input.GetKey(KeyCode.Space) && isJumping == false && transform.position.y - startPosition.y < 4.5f && body.velocity.y >= 0)
        {
            body.AddForce(Vector3.up * jumpBonus, ForceMode.Impulse);
        }

        if(Mathf.Abs(body.velocity.x) > BootsSpeed)
        {
            float newX = BootsSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3((float)newX, body.velocity.y, body.velocity.z);
        }

        if(Mathf.Abs(axis) < 0.1)
        {
            float newX = body.velocity.x * (1 - Time.deltaTime * 5f);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            BootsSpeed = BootsSpeed * 2;
            float newX = BootsSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3((float)newX, body.velocity.y, body.velocity.z);
        }
    
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            BootsSpeed = maxRunSpeed;
        }

        animComp.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        animComp.SetBool("isJumping", isJumping);

        Ray ray = new Ray(transform.position, Vector3.up);
        //Debug.DrawRay(transform.position, Vector3.up, Color.red);
        //Debug.Log((Vector3.up * RayLength).ToString());
        RaycastHit hit;

       if(Physics.Raycast(ray, out hit, RayLength))
        {
            Object = hit.collider.gameObject;
            if (Object.tag == "QuestionBox")
            {
                TextMeshProUGUI = GameObject.Find("Canvas/Coin").GetComponent<TextMeshProUGUI>();

                if (TextMeshProUGUI == null)
                {
                    Debug.Log("Error,TextMesh brick is null");
                }

                coin = coin + 1;
                if (coin < 10)
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

            if (Object.tag == "Brick")
            {
                TextMeshProUGUI = GameObject.Find("Canvas/Mario").GetComponent<TextMeshProUGUI>();
                SetScore(TextMeshProUGUI, 100);
                Destroy(Object);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeathBox"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("You Dead");
        }

        if(other.CompareTag("winBox"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("You Win");
        }
    }

    void SetScore(TextMeshProUGUI Text, int AddNum)
    {
        score = score + AddNum;
        int count = score;
        int digits = 0;

        while (count / 10 > 0)
        {
            digits++;
            count /= 10;
        }

        string TextDigits = "MARIO\n";
        for (int i = 0; i < (6 - digits); i++)
        {
            TextDigits = TextDigits + "0";
        }
        TextDigits = TextDigits + score.ToString();
        Text.text = TextDigits;
    }
}
