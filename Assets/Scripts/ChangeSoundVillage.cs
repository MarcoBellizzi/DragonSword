using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script usato per cambiare il sottofondo musicale
 * quando il player esce dal villagio
 */
public class ChangeSoundVillage : MonoBehaviour
{
    // clip della foresta
    [SerializeField] private AudioClip clip;

    private bool changed;
    
    void Start()
    {
        changed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!changed)
        {
            // stoppa la clip precedente del villaggio e fa partire quella della foresta
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
            GameObject.Find("Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Player").GetComponent<AudioSource>().Play();
            changed = true;
        }
    }
}
