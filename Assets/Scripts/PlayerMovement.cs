using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;

    private CharacterController controller;
    private Animator animator;
    private bool isAttacking;

    public int lifePoints;
    public Slider healthBar;

    private GameObject sfera;
    private bool lanciata;
    float y;

    void Start()
    {
        lifePoints = 100;
        healthBar.maxValue = lifePoints;
        
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        isAttacking = false;
        
        sfera = GameObject.Find("Sphere");
        lanciata = false;
    }
    
    void Update()
    {
        Move() ;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            sfera.transform.localScale *= 1.01f;
        }

       
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            y = 0.5f;
            lanciata = true;
        }

        if (lanciata)
        {
            
            sfera.GetComponent<CharacterController>().Move(transform.TransformDirection(new Vector3(0, y-=0.01f, 0.2f)));
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }


        if (lifePoints < 60)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow; 
        }
        if (lifePoints < 30)
        {
            healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red; 
        }
        
        
    }


    private void Move()
    {
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);  // diventano variabili globali

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

    private void OnCollisionEnter(Collision other)
    {
        
    }
}