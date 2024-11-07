using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class EmailService
{
    private const string API_URL = "http://localhost:8000/api/v1";
    public EmailData CurrentEmail { get; private set; }

    public IEnumerator FetchEmail()
    {
        string url = $"{API_URL}/minigame";
        Debug.Log($"[EmailService] Starting API call to: {url}"); // Debug log

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log("[EmailService] Sending request..."); // Debug log
            yield return request.SendWebRequest();
            Debug.Log("[EmailService] Request completed"); // Debug log

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[EmailService] API Error: {request.error}");
                Debug.LogError($"[EmailService] Response Code: {request.responseCode}");
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log($"[EmailService] Received JSON: {json}"); // Debug log

                try
                {
                    EmailResponse response = JsonConvert.DeserializeObject<EmailResponse>(json);
                    CurrentEmail = response.ToEmailData();
                    Debug.Log($"[EmailService] Successfully parsed email data"); // Debug log
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[EmailService] JSON Parsing Error: {e.Message}");
                }
            }
        }
    }
}