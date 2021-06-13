using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyMovement : MonoBehaviour
{
    public float lifePoints;
    
    private NavMeshAgent agent;
    private float detectingPlayerDistance;
    private float hittingPlayerDistance;
    private Vector3[] points;
    private int destPoint;
    private Vector3 moveDirection;
    private Animator animator;
    private GameObject player;
    private bool demaging;
    private bool attack;
    private bool die;
    private Random random;

    void Start()
    {
        detectingPlayerDistance = 12;
        hittingPlayerDistance = 2;
        animator = GetComponentInChildren<Animator>();
        attack = false;
        player = GameObject.Find("Player");
        lifePoints = 100;
        demaging = false;
        die = false;
        destPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        points = new Vector3[4];
        points[0] = transform.position + new Vector3(10, 0, 0);
        points[1] = transform.position + new Vector3(-10, 0, 0);
        points[2] = transform.position + new Vector3(0, 0, 10);
        points[3] = transform.position + new Vector3(0, 0, -10);
        random = new Random();
    }

    void Update()
    {
        if (!die && lifePoints <= 0)
        {
            die = true;
            StartCoroutine(Die());
        }

        if (!die)
        {
            Move();
        }
    }

    private void Move()
    {
        
        if (!demaging && Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(player.transform.position, transform.position) < hittingPlayerDistance)
        {
            StartCoroutine(Hit());
        }
        
        if (!attack)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < hittingPlayerDistance)
            {
                StartCoroutine(Attack());
            }
            else
            {
                if (Vector3.Distance(player.transform.position, transform.position) < detectingPlayerDistance)
                {
                    Walk();
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    agent.destination = points[destPoint];

                    if (Vector3.Distance(transform.position, points[destPoint]) < 2f)
                    {
                       destPoint = (destPoint + random.Next(1, points.Length)) % points.Length;
                    }
                    
                    // Idle();
                }
            }
        }
    }

    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 1);
        animator.SetTrigger("Attack");
        attack = true;

        player.GetComponent<PlayerMovement>().lifePoints -= 5;

        yield return new WaitForSeconds(0.9f);

        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0);
        attack = false;
        animator.SetFloat("Speed", 0);
    }

    
    private IEnumerator Hit()
    {
        demaging = true;
        lifePoints -= 40;
        
        yield return new WaitForSeconds(0.9f);
        
        demaging = false;
    }
    
    private IEnumerator Die()
    {
        transform.Rotate(90, 0, 0);
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);        
    }

}