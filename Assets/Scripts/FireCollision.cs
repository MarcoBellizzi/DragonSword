using UnityEngine;

public class FireCollision : MonoBehaviour
{
    private ParticleSystem fire;
    private GameObject player;
    void Start()
    {
        fire = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Player") && fire.isPlaying)
        {
            player.GetComponent<PlayerMovement>().lifePoints -= 0.1f;
        }
    }
}
