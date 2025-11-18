using UnityEngine;

/*
 * Script da attaccare alla sfera che il player puo lanciare
 */
public class PlayerSphere : MonoBehaviour
{
    // clip da riprodurre quando si lancia la sfera
    [SerializeField] private AudioClip clip;
    
    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        // la sfera si sposta in avanti
        transform.Translate(new Vector3(0,0,18f) * Time.deltaTime);

        // se le sfera si allontana troppo dal player, viene sitrutta
        if (Vector3.Distance(transform.position, _player.transform.position) > 50)
        {
            _player.GetComponent<PlayerMovement>().lanciata = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // se colpisce il drago gli toglie della vita
        if (other.gameObject.name.Equals("Dragon"))
        {
            DragonMovement movement = GameObject.Find("Dragon").GetComponent<DragonMovement>();
            
            // se il drago stava dormeno sveglialo e cambia il sottofondo musicale
            if (!movement.svegliato)
            {
                movement.svegliato = true;
                movement.healthBar.gameObject.SetActive(true);
                movement.animator.SetTrigger("svegliati");
                GameObject.Find("Player").GetComponent<AudioSource>().clip = clip;
                GameObject.Find("Player").GetComponent<AudioSource>().Play();
            }
            
            movement.lifePoints -= 20;
            _player.GetComponent<PlayerMovement>().lanciata = false;
            Destroy(gameObject);
        }

        // se colpisce il nemico gli toglie della vita
        if (other.gameObject.name.Contains("Enemy"))
        {
            _player.GetComponent<PlayerMovement>().lanciata = false;
            other.gameObject.GetComponent<EnemyMovement>().lifePoints -= 20;
            Destroy(gameObject);
        }
    }
    
}