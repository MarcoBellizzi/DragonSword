using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    private GameObject player;
    private bool lanciata;
    private Vector3 direzione;
    void Start()
    {
        player = GameObject.Find("Player");
        lanciata = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !lanciata)
        {
            transform.position = player.transform.position + player.transform.TransformDirection(new Vector3(0, 1, 1)); 
            direzione = player.transform.TransformDirection(new Vector3(0, 0, 0.4f));
            lanciata = true;
        }
       
        if (lanciata)
        {
            transform.Translate(direzione);
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 50)
        {
            transform.position = player.transform.position + new Vector3(0, -10, 0);
            lanciata = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            other.gameObject.GetComponent<EnemyMovement>().lifePoints -= 50;
        }
        transform.position = player.transform.position + new Vector3(0, -10, 0);
        lanciata = false;
    }
}
