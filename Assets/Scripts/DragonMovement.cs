using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*
 * Script da attaccare al Drago (boss finale)
 *
 * Il drago si trova in uno stato di sonno. Quando il Player si avvicina
 * o sparando colpisce il drago, egli si sveglia e inizia il combattimento.
 */
public class DragonMovement : MonoBehaviour
{
    [SerializeField] private GameObject sferaPrefab;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject panel;

    public Animator animator;
    public bool svegliato;
    public NavMeshAgent agent;
    public Slider healthBar;
    public float lifePoints;

    private GameObject sfera1;
    private GameObject sfera2;
    private ParticleSystem fire;
    private GameObject player;
    private bool attacking;
    private bool inCiclo;
    private bool inseguendo;
    private bool guardando;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        svegliato = false;
        attacking = false;
        fire = GetComponentInChildren<ParticleSystem>();
        lifePoints = 2000;
        healthBar.gameObject.SetActive(false);
        healthBar.maxValue = lifePoints;
        inCiclo = false;
        inseguendo = false;
        guardando = false;
    }

    void Update()
    {
        if (lifePoints < 1200f)
        {
            // la barra della vita diventa gialla
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
        }

        if (lifePoints < 600f)
        {
            // la barra della vita diventa rossa
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;
        }

        if (lifePoints <= 0)
        {
            // il player ha sconfitto il gioco e appare il menu di vittoria
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            panel.transform.Find("Text").GetComponent<Text>().text = "HAI VINTO!!";
            panel.SetActive(true);
        }

        healthBar.value = lifePoints;

        // se sta dormendo e il player si avvicina
        if (!svegliato && Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            // compare la barra della salute del drago
            healthBar.gameObject.SetActive(true);

            // il drago si sveglia e inizia l'animazione
            animator.SetTrigger("svegliati");
            svegliato = true;

            // cambia il sottofondo musicale 
            GameObject.Find("Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Player").GetComponent<AudioSource>().Play();
        }

        if (svegliato && !inCiclo)
        {
            StartCoroutine(Move());
        }

        if (guardando)
        {
            transform.parent.transform.LookAt(player.transform.position);
        }

        if (inseguendo)
        {
            if (!attacking)
            {
                agent.SetDestination(player.transform.position);

                // se si avvicina al player allora lo morde
                if (Vector3.Distance(transform.position, player.transform.position) < 11)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        // inizia l'animazione di attacco
        animator.SetTrigger("mordi");

        // toglie i life points al player
        player.GetComponent<PlayerMovement>().lifePoints -= 5;
        attacking = true;

        yield return new WaitForSeconds(1.05f);

        attacking = false;
    }

    private IEnumerator Move()
    {
        inCiclo = true;
        guardando = true;

        yield return new WaitForSeconds(5.5f);

        // inizia l'animazione dello fiammata
        guardando = false;
        animator.SetTrigger("sparaFuoco");
        
        yield return new WaitForSeconds(1f);

        // sputa fuoco (particle system)
        fire.Play();
        
        yield return new WaitForSeconds(2.2f);

        // finisce di sparare il fuoco
        fire.Stop();
        
        yield return new WaitForSeconds(1f);
        
        
        // inizia a seguirci
        animator.SetTrigger("inseguici");
        inseguendo = true;

        yield return new WaitForSeconds(20f);

        // inizia a volare
        GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<MeshCollider>().enabled = false;
        animator.SetTrigger("vola");
        inseguendo = false;
        guardando = true;
        
        yield return new WaitForSeconds(7f);

        // spara la prima palla di fuoco
        sfera1 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3.5f, 3)),
            transform.rotation);
        sfera1.transform.LookAt(player.transform.position);

        yield return new WaitForSeconds(2.7f);

        // spara la seconda palla di fuoco
        sfera2 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3.5f, 3)),
            transform.rotation);
        sfera2.transform.LookAt(player.transform.position);

        yield return new WaitForSeconds(3.6f);

        // termina il suo ciclo e ricomincia da capo
        GetComponent<BoxCollider>().enabled = true;
        GetComponentInChildren<MeshCollider>().enabled = true;
        inCiclo = false;
    }
}