using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraRotation : MonoBehaviour
{
    public Transform CenterPoint;

    public float Distance = 25f;

    private float SpeedX = 240f;
    public float SpeedY = 120f;
    //Angle limit
    private float MinLimity = 5;
    public float MaxLimity = 180;
    //rotation angle
    private float mX = 0.0f;
    private float mY = 0.0f;
    //mouse distance
    private float MaxDistance = 25f;
    private float MinDistance = 1.5f;

    public float ZoomSpeed = 1.5f;

    public bool isNeedDamping = true;

    private float Damping = 10f;

    private Quaternion mRotation;

    private Ray ray;
    private RaycastHit Hit;

    public GameObject Player;
    public GameObject Tower;

    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        mX = transform.eulerAngles.x;
        mY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit))
            {
                var Object = Hit.collider.gameObject;
                if (Object.tag == "Building")
                {
                    float mMoney = Player.GetComponent<PlayerData>().GetMoney();

                    if(mMoney >= 3)
                    {
                        Player.GetComponent<PlayerData>().buildTower(3f);
                        var GeneratePosition = Object.transform.Find("CenterPosition");
                        Instantiate(Tower, GeneratePosition.position, Tower.transform.rotation);
                    }
                    else
                    {
                        textMeshProUGUI.gameObject.SetActive(true);
                    }

                }
            }
        }


        if (CenterPoint != null && Input.GetMouseButton(1))
        {
            mX += Input.GetAxis("Mouse X") * SpeedX * 0.02f;
            mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02f;

            //limit the range
            mY = ClampAngle(mY, MinLimity, MaxLimity);

            mRotation = Quaternion.Euler(mY, mX, 0);
            if(isNeedDamping)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, mRotation,Time.deltaTime * Damping);
            }
            else
            {
                transform.rotation = mRotation;
            }
        }

        Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

        Vector3 mPosition = mRotation * new Vector3(0f,0f,-Distance) + CenterPoint.position;

        if(isNeedDamping)
        {
            transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
        }
        else
        {
            transform.position = mPosition;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
