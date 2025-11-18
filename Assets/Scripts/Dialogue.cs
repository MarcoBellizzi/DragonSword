using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Script da attaccare al dialog box per visualizzare la storia
 */
public class Dialogue : MonoBehaviour
{
    // componente in cui vengono visualizzati l'introduzione e la storia
    public TextMeshProUGUI textComponent;
    
    // array di stringe che contiene l'introduzione e la storia
    public string[] lines;
    
    // velocità di visualizzazione
    public float textSpeed;

    // l'indice dell array che scorre le frasi della storia
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            // se la frase è completa
            if(textComponent.text == lines[index])
            {
                // cambia frase
                NextLine();
            }
            else
            {
                // termina di riempire la frase
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    // visualizza progressivamente la frase
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        // se non è l'ultima frase
        if(index < lines.Length -1)
        {
            // passa alla frase successiva
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // disattiva il dialog box
            gameObject.SetActive(false);
        }
    }
}
