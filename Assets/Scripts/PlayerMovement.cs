using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public bool lanciata;
    public bool isPaused;
    
    private GameObject sfera;
    private float moveSpeed;
    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator animator;
    private bool isAttacking;

    void Start()
    {
        lifePoints = 100f;
        healthBar.maxValue = lifePoints;
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        isAttacking = false;
        text.text = "0";
        lanciata = false;
        GetComponent<AudioSource>().PlayOneShot(sottofondoRilassante);
        isPaused = false;
    }
    
    void Update()
    {
        if (lifePoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            panel.SetActive(true);
        }
        
        Move() ;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !lanciata)
        {
            lanciata = true;
            sfera = Instantiate(sferaPrefab, transform.position + transform.TransformDirection(new Vector3(0, 1, 0)), GameObject.Find("Main Camera").transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(suonoSfera);
        }

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

        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            Time.timeScale = 0;
            pausa.SetActive(true);
        }
        
        text.text = Managers.Inventory.GetItemCount("Salute").ToString();
        
        if (lifePoints > 60f)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green; 
        }
        if (lifePoints > 30f && lifePoints <= 60f)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow; 
        }
        if (lifePoints < 30f)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red; 
        }

        healthBar.value = lifePoints;

    }


    private void Move()
    {
        moveDirection = transform.TransformDirection(new Vector3(0, 0, Input.GetAxis("Vertical")));

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        } 
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        } 
        else if (moveDirection == Vector3.zero)
        {
            Idle(); 
        }
            
        moveDirection *= moveSpeed;
        moveDirection += new Vector3(0, -5f, 0);
        controller.Move(moveDirection * Time.deltaTime);

    }

    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
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