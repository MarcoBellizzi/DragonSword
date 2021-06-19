using UnityEngine;

/*
 * Script da attaccare al particole system che rappresenta il fuoco emesso dal drago
 */
public class FireCollision : MonoBehaviour
{
    private ParticleSystem fire;
    private GameObject player;
    void Start()
    {
        fire = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
    }

    // se il player si trova all'interno del fuoco perde vita
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Player") && fire.isPlaying)
        {
            player.GetComponent<PlayerMovement>().lifePoints -= 0.1f;
        }
    }
}
