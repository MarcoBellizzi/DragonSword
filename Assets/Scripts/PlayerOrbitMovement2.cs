using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOrbitMovement : MonoBehaviour
{
    [SerializeField] private Transform target;  // camera?
    
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

  //  private Vector3 moveDirection;

    private CharacterController controller;
    private Animator animator;
    private bool isAttacking;

    public int lifePoints;
    public Slider healthBar;

    void Start()
    {
        lifePoints = 100;
        healthBar.maxValue = lifePoints;
        
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        isAttacking = false;
    }
    
    void Update()
    {
        Move() ;
        
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
        
        Vector3 movement = Vector3.zero;
        
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        if (horInput != 0 || vertInput != 0)
        {
            
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            } 
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            } 
            
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, 1.5f * Time.deltaTime);
        }
        else
        {
            Idle();
        }

        movement += new Vector3(0, -5f, 0);
        controller.Move(movement * Time.deltaTime);

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