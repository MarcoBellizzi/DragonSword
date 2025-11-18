using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script da attaccare alla camera
 */
public class CameraController : MonoBehaviour
{
    void Start()
    {
        // nascondere il cursore
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}