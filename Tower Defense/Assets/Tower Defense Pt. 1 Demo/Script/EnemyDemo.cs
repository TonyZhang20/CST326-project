using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyDemo : MonoBehaviour
{
    public float MaxHealth = 5;
    public float health;

    public float coinWorth = 1;

    public GameObject Player;

    public Slider slider;
    public GameObject healthBarUI;

    private Animator animator;
    private int targetWaypointIndex;

    public Transform[] TargetList;
    public Transform[] TargetListT;
    public Transform Target;

    public AudioClip audioClip;

    private NavMeshAgent agent;

    private Transform[] currentList;

    private Vector3 TargetPosition;

    AudioSource audioSource;
    //-----------------------------------------------------------------------------
    void Start()
    {
        animator = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();

        //mRotation = transform.rotation;
        //targetWaypointIndex = 0;
        float randomNumber = Random.Range(0, 4);
        
        if(randomNumber <= 2)
        {
            currentList = TargetListT;
            health = 3;
            MaxHealth = 3;
        }
        else
        {
            currentList = TargetList;
        }

        //health = MaxHealth;
        slider.value = CalculateHealth();

        healthBarUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    //-----------------------------------------------------------------------------
    void Update()
    {

        slider.value = CalculateHealth();
        
        if(health < MaxHealth)
        {
            audioSource.Play();
            healthBarUI.SetActive(true);
        }

        if (targetWaypointIndex < currentList.Length)
        {
            TargetPosition = GetNaveMeshPosition(currentList[targetWaypointIndex].position);
            agent.SetDestination(TargetPosition);
        }

        if (targetWaypointIndex < currentList.Length / 2)
        {
            animator.SetTrigger("Run");
        }

        if (targetWaypointIndex >= currentList.Length / 2)
        {
            animator.SetTrigger("RunFast");
            agent.speed = 4;
        }


        if(health <= 0)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.clip = audioClip;
            source.Play();
            agent.speed = 0;
            animator.SetTrigger("Death");
        }
    }

    //-----------------------------------------------------------------------------
    public void takeDamage(float Damage)
    {
        slider.value = CalculateHealth();

        health -= Damage;
    }
    public void Death(float worthCoin)
    {
        Player.GetComponent<PlayerData>().SendMessage("addMoney", coinWorth);
        
        Destroy(gameObject);
    }

    Vector3 GetNaveMeshPosition(Vector3 samplePosition)
    {
        NavMesh.SamplePosition(samplePosition, out NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("GetDamage") && health > 0)
        {
            Player.GetComponent<PlayerData>().SendMessage("takeDamage", 1);
            animator.SetTrigger("Win");
        }

        if (other.CompareTag("wayPoint"))
        {
            targetWaypointIndex = targetWaypointIndex + 1;

            if (targetWaypointIndex >= currentList.Length)
            {
                agent.SetDestination(Target.position);
            }

        }

        if(other.CompareTag("Tower"))
        {
            other.GetComponentInParent<Tower>().EnterTrigger(gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            other.GetComponentInParent<Tower>().LeaveTrigger(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            other.GetComponentInParent<Tower>().EnterTrigger(gameObject);
        }
    }

    private float CalculateHealth()
    {
        return health / MaxHealth;
    }
}
