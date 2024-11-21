using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float hp = 100;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public GameObject[] path;
    public GameObject bullet;
    [Range(0, 20)] public float detectionDistance = 2;
    [HideInInspector] public int currentPoint = 0;
    [HideInInspector] public int currentDirection = 1; //1 - вперед -1 - назад
    public float cooldown = 0.5f;
    public float cooldownTime = 0.5f;

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(path[0].transform.position);
        GetComponent<NavMeshAgent>().updateRotation = false;
    }

    //бачить гравця коли:
    //1. дивиться саме на гравця
    //2. має стояти під правильним кутом до гравця
    //3. дистанція до гравця
    //4. враховувати перешкоди

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        var distance = Vector3.Distance(transform.position, path[currentPoint].transform.position);
        if (distance < 2)
        {
            currentPoint += currentDirection;
            if (currentPoint == 4 || currentPoint == -1)
            {
                if (currentDirection == 1)
                {
                    currentPoint = 2;
                    currentDirection = -1;
                }
                else if (currentDirection == -1)
                {
                    currentPoint = 1;
                    currentDirection = 1;
                }
            }

            GetComponent<NavMeshAgent>().SetDestination(path[currentPoint].transform.position);
        }
    }

    void CheckForPlayer()
    {
        var enemyPosition = transform.position;
        var playerPosition = GameObject.FindWithTag("Player").transform.position;
        enemyPosition.z = 0;
        playerPosition.z = 0;
        float distance = Vector3.Distance(enemyPosition, playerPosition);


        if (distance < detectionDistance)
        {
            Debug.Log("дистанція норм!! ");


            var hit = Physics2D.Raycast(transform.position, (playerPosition - transform.position).normalized,
                distance + 1);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player")&& Time.time >= cooldownTime)
                {
                    var bulletPosition = transform.position;
                    var direction = playerPosition - bulletPosition;
                    var bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
                    bulletObject.GetComponent<bullet>().direction = direction.normalized;
                    bulletObject.GetComponent<bullet>().owner = gameObject;
                    bulletObject.GetComponent<bullet>().Shoot();
                    GetComponent<AudioSource>().Play();
                    cooldownTime = Time.time + cooldown;
                }
            }
        }
        // if () {
        // } 
    }

    public void TakeDamage()
    {
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>().Play();
    }

    public void FatalDamage()
    {
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
    }
}