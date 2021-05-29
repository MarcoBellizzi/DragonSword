using UnityEngine;

public class DragonSphere : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0,0,0.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            other.GetComponent<PlayerMovement>().lifePoints -= 20;
            Destroy(gameObject);
        }
    }
}
