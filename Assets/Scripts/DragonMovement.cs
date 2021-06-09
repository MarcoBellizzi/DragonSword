using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] private GameObject sferaPrefab;
    private GameObject sfera1;
    private GameObject sfera2;
    private ParticleSystem fire;
    public Animator animator;
    private GameObject player;
    public bool svegliato;
    private bool attacking;
    public NavMeshAgent agent;
    private int cont;
    public Slider healthBar;
    public float lifePoints;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        svegliato = false;
        cont = 0;
        attacking = false;
        fire = GetComponentInChildren<ParticleSystem>();
        lifePoints = 2000;
        healthBar.gameObject.SetActive(false);
        healthBar.maxValue = lifePoints;
    }

    void Update()
    {

        healthBar.value = lifePoints;
        if (!svegliato && Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            healthBar.gameObject.SetActive(true);
            animator.SetTrigger("svegliati");
            svegliato = true;
        }

        if (svegliato)
        {
            cont++;

            if (cont < 400)
            {
                transform.parent.transform.LookAt(player.transform.position);
            }

            if (cont == 400)
            {
                fire.Play();
                animator.SetTrigger("sparaFuoco");
            }

            if (cont == 620)
            {
                fire.Stop();
                animator.SetTrigger("inseguici");
            }

            if (cont > 620 && cont < 2000)
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
            }

            if (cont == 2500)
            {
                sfera1 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3, 3)), transform.rotation);
                sfera1.transform.LookAt(player.transform.position);
            }
            
            if (cont == 2560)
            {
                sfera2 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3, 3)), transform.rotation);
                sfera2.transform.LookAt(player.transform.position);
            }
            
            if (cont == 2600)
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
