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
    private int cont;

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
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
            GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(clip);
        }

        if (svegliato)
        {
            cont++;

            // guarda il player
            if (cont < 400)
            {
                transform.parent.transform.LookAt(player.transform.position);
            }

            // sputa fuoco (particle system)
            if (cont == 400)
            {
                fire.Play();
                animator.SetTrigger("sparaFuoco");
            }

            // finisce di sparare il fuoco e inizia e inseguirci
            if (cont == 620)
            {
                fire.Stop();
                animator.SetTrigger("inseguici");
            }

            // si muove in direzione del player
            if (cont > 620 && cont < 2000)
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

            // inizia a volare
            if (cont == 2000)
            {
                animator.SetTrigger("vola");
            }

            // insegue il player volando
            if (cont > 2000 && cont < 2600)
            {
                agent.SetDestination(player.transform.position);
            }

            // spara la prima palla di fuoco
            if (cont == 2500)
            {
                sfera1 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3, 3)), transform.rotation);
                sfera1.transform.LookAt(player.transform.position);
            }
            
            // spara la seconda palla di fuoco
            if (cont == 2560)
            {
                sfera2 = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 3, 3)), transform.rotation);
                sfera2.transform.LookAt(player.transform.position);
            }
            
            // termina il suo ciclo e ricomincia da capo
            if (cont == 2600)
            {
                cont = 0;
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
}
