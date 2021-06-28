using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/*
 * Script usato per cambiare il sottofondo musicale
 * quando il player esce dal villagio
 */
public class ChangeSoundVillage : MonoBehaviour
{
    // clip della foresta
    [SerializeField] private AudioClip clip;

    private bool changed;
    private Random random;
    void Start()
    {
        changed = false;
        random = new Random();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!changed)
        {
            // assegna ad un nemico casuale la chiave per aprire il cancello
            EnemyMovement[] nemici = GameObject.FindObjectsOfType<EnemyMovement>();
            int num = random.Next(0, nemici.Length);
            nemici[num].hasKey = true;
            
            // stoppa la clip precedente del villaggio e fa partire quella della foresta
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
            GameObject.Find("Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Player").GetComponent<AudioSource>().Play();
            changed = true;
        }
    }
}
