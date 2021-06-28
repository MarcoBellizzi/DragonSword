using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

/*
 * Script da attaccare ai nemici presenti nel bosco (scheletrini)
 *
 * I nemici vagano per il basco, se il player si avvicina a loro, inseguiranno il player
 * e lo attaccheranno con la loro arma corpo a corpo.
 *
 * Quando vengono sconfitti lasciano una pozione che il player puo raccogliere per curarsi.
 *
 * Uno di loro possiede una chiave segreta per aprire il cancello
 */
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject chiave;
    [SerializeField] private GameObject salute;
    
    public float lifePoints;
    public bool hasKey;
    
    private NavMeshAgent agent;
    private float detectingPlayerDistance;
    private float hittingPlayerDistance;
    
    // punti in cui i nemici si muoveranno casualmente all'interno del bosco
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
        animator.SetFloat("Speed", 0.5f);
        
        // i punti vengono generati a partire dalla posizione iniziale del nemico
        // in alto, in basso, a sinistra e a destra
        points = new Vector3[4];
        points[0] = transform.position + new Vector3(10, 0, 0);
        points[1] = transform.position + new Vector3(-10, 0, 0);
        points[2] = transform.position + new Vector3(0, 0, 10);
        points[3] = transform.position + new Vector3(0, 0, -10);
        
        random = new Random();
        hasKey = false;

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
        // se il player sta attaccando con la spada e si trova abbastanza vicino, allora il nemico viene colpito
        if (!demaging && Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(player.transform.position, transform.position) < hittingPlayerDistance)
        {
            StartCoroutine(Hit());
        }
        
        if (!attack)
        {
            // se il player si trova abbastanza vicino il nemico lo attacca
            if (Vector3.Distance(player.transform.position, transform.position) < hittingPlayerDistance)
            {
                StartCoroutine(Attack());
            }
            else
            {
                // se il player si trovo vicino lo insegue
                if (Vector3.Distance(player.transform.position, transform.position) < detectingPlayerDistance)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    // raggiungi un punto casuale
                    agent.destination = points[destPoint];

                    // se hai raggiunto un punto casuale allora cambia punto casualmente
                    if (Vector3.Distance(transform.position, points[destPoint]) < 2f)
                    {
                       destPoint = (destPoint + random.Next(1, points.Length)) % points.Length;
                    }
                }
            }
        }
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
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(3f);

        if (hasKey)
        {
            Instantiate(chiave, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
        else
        {
            Instantiate(salute, transform.position + new Vector3(0, 0.5f, 0), new Quaternion(90, 0, 0, 0));
        }
        
        Destroy(gameObject);        
    }

}