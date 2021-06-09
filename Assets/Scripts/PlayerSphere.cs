using UnityEngine;

public class PlayerSphere : MonoBehaviour
{
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
            Debug.Log(other.gameObject.GetComponent<DragonMovement>().svegliato.ToString());
            // capire perchè da un errore in console
            other.gameObject.GetComponent<DragonMovement>().svegliato = true;
            other.gameObject.GetComponent<DragonMovement>().healthBar.gameObject.SetActive(true);
            other.gameObject.GetComponent<DragonMovement>().animator.SetTrigger("svegliati");
            other.gameObject.GetComponent<DragonMovement>().lifePoints -= 20;
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