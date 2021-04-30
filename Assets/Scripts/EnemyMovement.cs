using System;
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
    private bool attack;
    private GameObject player;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        attack = false;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Move() ;
    }

    private void Move()
    {
        if (!attack)
        {
            if (Vector3.Distance(player.transform.position, controller.transform.position) < 1.9f)
            {
                StartCoroutine(Attack());
            }
            else
            {
                if (Vector3.Distance(player.transform.position, controller.transform.position) < 8f)
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
        player.GetComponent<PlayerMovement>().lifePoints -= 10;
        Debug.Log("player attaccato: " + player.GetComponent<PlayerMovement>().lifePoints);

        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 1);
        animator.SetTrigger("Attack");
        attack = true;
        
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0);
        attack = false;
    }
}