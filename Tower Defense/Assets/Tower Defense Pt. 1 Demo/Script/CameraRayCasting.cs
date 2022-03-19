using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCasting : MonoBehaviour
{
    public float Damange = 1;
    // Start is called before the first frame update
    private Ray ray;
    private RaycastHit Hit;
    private GameObject Object;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit))
            {
                Object = Hit.collider.gameObject;
                if(Object.tag == "Monster")
                {
                    GameObject.FindGameObjectWithTag("Monster").GetComponent<EnemyDemo>().SendMessage("takeDamage", Damange);
                }
            }
        }

    }
}
