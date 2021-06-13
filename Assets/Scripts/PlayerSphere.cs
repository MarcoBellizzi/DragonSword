using UnityEngine;

public class PlayerSphere : MonoBehaviour
{

    [SerializeField] private AudioClip clip;
    
    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(new Vector3(0,0,0.4f));

        if (Vector3.Distance(transform.position, _player.transform.position) > 50)
        {
            _player.GetComponent<PlayerMovement>().lanciata = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Dragon"))
        {

            DragonMovement movement = GameObject.Find("Dragon").GetComponent<DragonMovement>();
            if (!movement.svegliato)
            {
                movement.svegliato = true;
                movement.healthBar.gameObject.SetActive(true);
                movement.animator.SetTrigger("svegliati");
                GameObject.Find("Player").GetComponent<AudioSource>().Stop();
                GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(clip);
            }

            movement.lifePoints -= 20;
            _player.GetComponent<PlayerMovement>().lanciata = false;
            Destroy(gameObject);
        }

        if (other.gameObject.name.Contains("Enemy"))
        {
            _player.GetComponent<PlayerMovement>().lanciata = false;
            other.gameObject.GetComponent<EnemyMovement>().lifePoints -= 20;
            Destroy(gameObject);
        }
    }
    
}