using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] private GameObject sferaPrefab;
    private ParticleSystem fire;
    public Animator animator;
    private GameObject player;
    public bool svegliato;
    private bool attacking;
    public NavMeshAgent agent;
    private int cont;
    public float lifePoints;

    private GameObject prova;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        svegliato = false;
        cont = 0;
        attacking = false;
        fire = GetComponentInChildren<ParticleSystem>();
        lifePoints = 2000;

        prova = new GameObject();
    }

    void Update()
    {
        if (!svegliato && Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            animator.SetTrigger("svegliati");
            svegliato = true;
        }

        if (svegliato)
        {
            cont++;

            if (cont < 400)
            {
        //        transform.LookAt(player.transform.position);
            }
            
            if (cont == 400)
            {
                fire.Play();
                animator.SetTrigger("sparaFuoco");
            }

            if (cont == 640)
            {
                fire.Stop();
                animator.SetTrigger("inseguici");
           //     transform.LookAt(player.transform.position);
            }

            if (cont > 640 && cont < 2000)
            {
                if (!attacking)
                {
                    agent.SetDestination(player.transform.position);
                    if (Vector3.Distance(transform.position, player.transform.position) < 11)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }

            if (cont == 2000)
            {
                animator.SetTrigger("vola");
            }

            if (cont > 2000 && cont < 2600)
            {
                agent.SetDestination(player.transform.position);
          //      transform.LookAt(player.transform);
            }

            if (cont == 2500 || cont == 2560)
            {
                Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 2, 3)), transform.rotation);
            }

            if (cont == 2600)
            {
          //      transform.LookAt(new Vector3(-107, transform.position.y, 157));
            }
            
            if (cont == 3000)
            {
                cont = 0;
            }
        }
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("mordi");
        player.GetComponent<PlayerMovement>().lifePoints -= 5;
        attacking = true;
        
        yield return new WaitForSeconds(1.05f);

        attacking = false;
    }
}
