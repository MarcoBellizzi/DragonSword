using UnityEngine;

/*
 * Script da attaccare alle palle di fuoco del drago
 */
public class DragonSphere : MonoBehaviour
{
    void Update()
    {
        // la palla di fuoco si sposta in avanti
        transform.Translate(new Vector3(0,0,18f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // se colpisce il player ci toglie della vita
        if (other.name.Equals("Player"))
        {
            other.GetComponent<PlayerMovement>().lifePoints -= 20;
            Destroy(gameObject);
        }
    }
}
