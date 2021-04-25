using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private float walkSpeed;

    private Vector3 moveDirection;

    private CharacterController controller;
    private Animator animator;

    public NavMeshAgent agent;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Move() ;

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        StartCoroutine(Attack());
    //    }
    }

    private void Move()
    {
        GameObject player = GameObject.Find("Player");

        if (Vector3.Distance(player.transform.position, controller.transform.position) < 2f)
        {
            StartCoroutine(Attack());
        }
        else
        {
            if (Vector3.Distance(player.transform.position, controller.transform.position) < 6f)
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
        animator.SetLayerWeight(animator.GetLayerIndex("Attack layer"), 1);
        animator.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack layer"), 0);
    }
}