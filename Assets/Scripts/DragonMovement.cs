using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem fire;
    private Animator animator;
    private GameObject player;
    private bool svegliato;

    private int cont;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
        svegliato = false;
        cont = 0;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            animator.SetTrigger("svegliati");
            svegliato = true;
        }

        if (svegliato)
        {
            cont++;
            
            transform.LookAt(player.transform);

            if (cont == 270)
            {
                animator.SetTrigger("sparaFuoco");
                fire.Play();
            }

            if (cont > 270 && cont < 440)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 50)
                {
                    player.GetComponent<PlayerMovement>().lifePoints -= 0.1f;
                    // capire perchè non va sullo 0.1f
                    player.GetComponent<PlayerMovement>().healthBar.value -= 1f;
                    
                    Debug.Log(Vector3.Distance(transform.position, player.transform.position));
                    Debug.Log(player.gameObject.GetComponent<PlayerMovement>().lifePoints);
                }
            }

            if (cont == 440)
            {
                fire.Stop();
            }
            
            Debug.Log(cont);
        }
    }
}
