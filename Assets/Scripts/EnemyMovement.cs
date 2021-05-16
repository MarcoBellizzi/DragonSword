using System;
using System.Collections;
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
    private float lifePoints;
    private bool demaging;
    private bool die;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        attack = false;
        player = GameObject.Find("Player");
        lifePoints = 100;
        demaging = false;
        die = false;
    }

    void Update()
    {
        if (lifePoints <= 0)
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
        
        if (!demaging && Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(player.transform.position, controller.transform.position) < 1.9f)
        {
            StartCoroutine(Hit());
        }
        
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
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 1);
        animator.SetTrigger("Attack");
        attack = true;

        // da aggiustare.. non va benissimo il fatto che inizia l 'attacco, toglie la vita, finisce l'attacco
        // sembra che il finisce l'attacco non va benissimo e toglie piu velocemente vita rispetto alla
        // velocità d'attacco
        
        yield return new WaitForSeconds(0.45f);
        
        player.GetComponent<PlayerMovement>().lifePoints -= 5;
        player.GetComponent<PlayerMovement>().healthBar.value -= 5;

        StartCoroutine(EndAttack());
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.45f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0);
        attack = false;
    }
    

    private IEnumerator Hit()
    {
        demaging = true;
        lifePoints -= 40;
        Debug.Log(lifePoints);
        yield return new WaitForSeconds(0.9f);
        demaging = false;
    }
    
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);        
    }
}