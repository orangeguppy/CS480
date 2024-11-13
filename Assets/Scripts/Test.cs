using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hello");
        StartCoroutine(GetRequest("https://phishfindersrealforrealsbs.org:8001/"));
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }
}