using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float detectingPlayerDistance;
    [SerializeField] private float hittingPlayerDistance;

    private Vector3 moveDirection;
    private Animator animator;
    
    public NavMeshAgent agent;
    
    private GameObject player;
    
    public float lifePoints;
    private bool demaging;
    private bool attack;
    private bool die;
    
    
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attack = false;
        player = GameObject.Find("Player");
        lifePoints = 100;
        demaging = false;
        die = false;
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
                    Idle();
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
        player.GetComponent<PlayerMovement>().healthBar.value -= 5;

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