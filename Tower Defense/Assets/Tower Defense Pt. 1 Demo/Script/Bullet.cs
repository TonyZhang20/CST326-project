using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    private GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            var direction = Target.transform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * Speed, Space.World);
        }
    }

    public void GetTarget(GameObject target)
    {
        Target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<EnemyDemo>().takeDamage(1f);
            Destroy(gameObject);
        }
    }
}
