using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public static class WebMethod
{
    public static readonly string GET = "GET";
    public static readonly string POST = "POST";
    public static readonly string PUT = "PUT";  
    public static readonly string DELETE = "DELETE";    
}

public class WebRequestHandler : MonoBehaviour
{
    public string baseUrl = "https://pokeapi.co/api/v2/pokemon";
    public string method = WebMethod.GET;
    public string result = string.Empty;
    public  string payload = string.Empty;

    public IEnumerator ExecuteRequest(string endpoint)
    {
        string url = baseUrl + "/" + endpoint;
        UnityWebRequest webRequest = new UnityWebRequest(url);

        webRequest.method = method;
        if (method == WebMethod.POST)
        {
            byte[] dataRaw = Encoding.UTF8.GetBytes(payload);
            UploadHandlerRaw uploadHandler = new UploadHandlerRaw(dataRaw);
            webRequest.uploadHandler = uploadHandler;
        }
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");


        yield return webRequest.SendWebRequest();
        try
        {
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            Debug.Log("Request success:" + webRequest.responseCode);
            result = Encoding.ASCII.GetString(webRequest.downloadHandler.data);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}