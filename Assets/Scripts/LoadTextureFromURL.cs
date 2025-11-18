using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Script da attaccare ad un cubo in cui viene caricata un immagine presa da internet
 */
public class LoadTextureFromURL : MonoBehaviour
{
    private string TextureURL = "https://imgix.ranker.com/list_img_v2/15198/2735198/original/life-of-a-medieval-knight?w=817&h=427&fm=jpg&q=50&fit=crop";

    void Start()
    {
        StartCoroutine(DownloadImage(TextureURL));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}