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

    void Update()
    {
        // prendere le coordinate del mouse
        float mouseX = Input.GetAxis("Mouse X") * 300 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 300 * Time.deltaTime;

        // routare il player in orizzontale
        transform.parent.Rotate(Vector3.up, mouseX);

        // routare la camera in verticale
        transform.Rotate(Vector3.left, mouseY);
    }
}