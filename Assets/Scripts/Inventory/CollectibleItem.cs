using System.Collections;
using UnityEngine;

/*
 *  Script da attaccare agli oggetti che si desidera inserire nell'inventory
 */
public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            Managers.Inventory.AddItem(itemName);
            Destroy(this.gameObject);
        }
    }
}
