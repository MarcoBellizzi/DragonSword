using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVillage : MonoBehaviour
{

    [SerializeField] private AudioClip clip;

    private bool changed;
    
    void Start()
    {
        changed = false;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!changed)
        {
            GameObject.Find("Player").GetComponent<AudioSource>().Stop();
            GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
