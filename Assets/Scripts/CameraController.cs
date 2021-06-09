using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 300 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 300 * Time.deltaTime;

        transform.parent.Rotate(Vector3.up, mouseX);

        //if ((mouseY < 0 && transform.rotation.x > -0.025f) || ( mouseY > 0 && transform.rotation.x < 0.025))
        //{
        transform.Rotate(Vector3.left, mouseY);
        //} 
    }
}