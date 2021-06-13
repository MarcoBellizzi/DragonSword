﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
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
            transform.Translate(0,-0.035f,0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && Managers.Inventory.GetItemCount("Key") > 0 && !apri)
        {
            GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(clip);
            apri = true;
        }
    }
}