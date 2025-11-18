using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script da attaccare alla porta del castello
 */
public class DoorMovement : MonoBehaviour
{
    // musica di sottofondo nel castello
    [SerializeField] private AudioClip clip;
    
    private bool apri;

    void Start()
    {
        apri = false;
    }

    
    void Update()
    {
        if (apri && transform.position.y > -10.4)
        {
            // scorri la porta verso il basso
            transform.Translate(0,-0.035f,0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // se il player si avvicina e possiede la chiave
        if (other.gameObject.name == "Player" && Managers.Inventory.GetItemCount("Key") > 0 && !apri)
        {
            // consuma la chiave e inizia a far abbassare la porta
            Managers.Inventory.ConsumeItem("Key");
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
            GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(clip);
            apri = true;
        }
    }
}
