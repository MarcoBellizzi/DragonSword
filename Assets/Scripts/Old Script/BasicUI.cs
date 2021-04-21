using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 40, 20), "Test"))
        {
            Debug.Log("Testo");
        }

        GUI.Label(new Rect(10, 20, 40, 20), "Label");
    }
}
