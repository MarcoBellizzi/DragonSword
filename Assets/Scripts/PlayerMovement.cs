using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/*
 * Script da attaccare al Player.
 *
 * Il player Si sposta in avanti e in dietro con i tasti W e S
 * Corre se si tiene premuto il tasto Shift
 * Con il tasto sinistro del mouse attacca con la spada
 * Con il tasto destro del mouse lascia una sfera che puo infliggere danno ai nemici se colpiti
 */
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private GameObject sferaPrefab;
    [SerializeField] private AudioClip suonoSfera;
    [SerializeField] private AudioClip sottofondoRilassante;
    [SerializeField] private AudioClip salute;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject pausa;
    
    public float lifePoints;
    public Slider healthBar;
    public Text text;
    public Text chiavi;
    public bool lanciata;
    public bool isPaused;
    
    private GameObject sfera;
    private float moveSpeed;
    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator animator;
    private bool isAttacking;
    private Random random;

    void Start()
    {
        lifePoints = 100f;
        healthBar.maxValue = lifePoints;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        isAttacking = false;
        text.text = "0";
        chiavi.text = "0";
        lanciata = false;
        isPaused = false;
        random = new Random();
        
        // inizia a riprodurre la musica di sottofondo del villagio
        GetComponent<AudioSource>().PlayOneShot(sottofondoRilassante);
        
        // assegna ad un nemico casuale la chiave per aprire il cancello
        EnemyMovement[] nemici = GameObject.FindObjectsOfType<EnemyMovement>();
        int num = random.Next(0, nemici.Length);
        nemici[num].hasKey = true;
    }
    
    void Update()
    {
        // se i life points scendono sotto zero il gioco termina e appare il menu di sconfitta
        if (lifePoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            panel.SetActive(true);
        }
        
        // muove il player
        Move() ;

        // se viene premuto il tasto destro del muose attacca con la spada
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        // se viene premuto il tasto sinistro del mouse lancia la sfera
        if (Input.GetKeyDown(KeyCode.Mouse1) && !lanciata)
        {
            lanciata = true;
            sfera = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 1, 0)), GameObject.Find("Main Camera").transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(suonoSfera);
        }

        // se viene premuto il tasto H consume una salute che ripristina un po di vita
        if (Input.GetKeyDown(KeyCode.H) && Managers.Inventory.GetItemCount("Salute") > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(salute);
            lifePoints += 20f;
            if (lifePoints > 100f)
            {
                lifePoints = 100f;
            }
            Managers.Inventory.ConsumeItem("Salute");
        }

        // se viene premuto il tasto P si mette in pausa il gioco
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            Time.timeScale = 0;
            pausa.SetActive(true);
        }
        
        // aggiorna i valori delle saluti e della chiave nell'interfaccia
        text.text = Managers.Inventory.GetItemCount("Salute").ToString();
        chiavi.text = Managers.Inventory.GetItemCount("Key").ToString();
        
        if (lifePoints > 60f)
        {
            // la barra della vita diventa verde
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green; 
        }
        if (lifePoints > 30f && lifePoints <= 60f)
        {
            // la barra della vita diventa gialla
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow; 
        }
        if (lifePoints < 30f)
        {
            // la barra della vita diventa rossa
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red; 
        }

        // aggiorna il riempimento della barra della vita
        healthBar.value = lifePoints;

    }


    private void Move()
    {
        // prende da da input (W o S) la direzione dove si desidera andare
        moveDirection = transform.TransformDirection(new Vector3(0, 0, Input.GetAxis("Vertical")));

        // se si va in avanti o in dietro e non si preme Shift cammina
        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
            animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        } 
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            // se si va in avanti o in dietro e si preme Shift corre
            moveSpeed = runSpeed;
            animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        } 
        else if (moveDirection == Vector3.zero)
        {
            // non si desidera muoversi il player resta fermo
            animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        }
            
        moveDirection *= moveSpeed;
        moveDirection += new Vector3(0, -5f, 0);
        controller.Move(moveDirection * Time.deltaTime);

    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        
        animator.SetLayerWeight(animator.GetLayerIndex("Attack"), 0);
        isAttacking = false;
    }
    
}