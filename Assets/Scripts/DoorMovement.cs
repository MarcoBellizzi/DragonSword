using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{

    private bool apri;
    // Start is called before the first frame update
    void Start()
    {
        apri = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (apri && transform.position.y > -10.4)
        {
            transform.Translate(0,-0.02f,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && Managers.Inventory.GetItemCount("Key") > 0)
        {
            apri = true;
        }
    }
}
